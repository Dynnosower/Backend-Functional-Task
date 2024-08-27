using System.Diagnostics;
using System.Text.Json;
using CarrierRates.Api.Data;
using CarrierRates.Api.Dtos.Dhl;
using CarrierRates.Api.Dtos.Lalamove;
using CarrierRates.Api.Entities;
using CarrierRates.Api.Factories;
using CarrierRates.Api.Mapping;
using CarrierRates.Api.Strategies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarrierRates.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierController : ControllerBase
    {
        private readonly CarrierStrategyFactory _factory;

        public CarrierController(CarrierStrategyFactory factory)
        {
            _factory = factory;
        }

        [Consumes("application/json")]
        [HttpGet("")]
        public IActionResult GetCarriers(CarrierRatesContext dbContext)
        {
            var carrierList = dbContext.Carriers.ToList<Carrier>();
            return Ok(carrierList);
        }

        [Consumes("application/json")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarrier(int id, bool isEnabled, CarrierRatesContext dbContext)
        {
            var existingCarrier = await dbContext.Carriers.FindAsync(id);

            if (existingCarrier is null)
            {
                return NotFound("Carrier does not exist.");
            }

            dbContext.Entry(existingCarrier)
            .CurrentValues
            .SetValues(new Carrier()
            {
                Id = existingCarrier.Id,
                Name = existingCarrier.Name,
                isEnabled = isEnabled,
                UpdatedAt = DateOnly.FromDateTime(DateTime.Now)
            });

            await dbContext.SaveChangesAsync();

            return Ok("Carrier updated.");
        }

        [Consumes("application/json")]
        [HttpPost("rates")]
        public async Task<IActionResult> GetRates(string carrierType, JsonElement request)
        {
            var strategy = _factory.CreateStrategy(carrierType);

            switch (strategy)
            {
                case DhlStrategy dhlStrategy:
                    {
                        try
                        {
                            DhlPostRatesRequestDto requestDto = JsonSerializer.Deserialize<DhlPostRatesRequestDto>(request.GetRawText())!;
                            var response = await dhlStrategy.PostRatesAsync(requestDto);
                            return Ok(response.ToShippingRateResponseDto());
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e);
                            return BadRequest("Bad request.");
                        }
                    }

                case LalamoveStrategy lalamoveStrategy:
                    {
                        try
                        {
                            LalamovePostRatesRequestDto requestDto = JsonSerializer.Deserialize<LalamovePostRatesRequestDto>(request.GetRawText())!;
                            var response = await lalamoveStrategy.PostRatesAsync(requestDto);
                            return Ok(response.ToShippingRateResponseDto());
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e);
                            return BadRequest("Bad request.");
                        }
                    }

                default:
                    return StatusCode(StatusCodes.Status500InternalServerError, "Invalid carrier");
            }
        }
    }
}
