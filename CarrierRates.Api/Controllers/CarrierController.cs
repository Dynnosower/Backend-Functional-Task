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
            var carrierList = dbContext.Carriers.ToList();
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
            else if ((existingCarrier.isEnabled && isEnabled) || (!existingCarrier.isEnabled && !isEnabled))
            {
                return StatusCode(StatusCodes.Status304NotModified, "Carrier unchanged.");
            }

            var carrierList = dbContext.Carriers.ToList();
            int enabledCarriers = 0;
            int soleCarrierId = 0;

            carrierList.ForEach(c =>
            {
                if (c.isEnabled)
                {
                    enabledCarriers++;
                    soleCarrierId = c.Id;
                }
            });

            if (enabledCarriers == 1 && !isEnabled)
            {
                var soleCarrier = await dbContext.Carriers.FindAsync(soleCarrierId);
                if (existingCarrier == await dbContext.Carriers.FindAsync(soleCarrierId))
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "Unable to disable carrier.");
                }
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
        public async Task<IActionResult> GetRates(string carrierName, JsonElement request)
        {
            try
            {
                var strategy = _factory.CreateStrategy(carrierName);

                switch (strategy)
                {
                    case DhlStrategy dhlStrategy:
                        {
                            DhlPostRatesRequestDto requestDto = JsonSerializer.Deserialize<DhlPostRatesRequestDto>(request.GetRawText())!;
                            var response = await dhlStrategy.PostRatesAsync(requestDto);
                            return Ok(response.ToShippingRateResponseDto());
                        }

                    case LalamoveStrategy lalamoveStrategy:
                        {
                            LalamovePostRatesRequestDto requestDto = JsonSerializer.Deserialize<LalamovePostRatesRequestDto>(request.GetRawText())!;
                            var response = await lalamoveStrategy.PostRatesAsync(requestDto);
                            return Ok(response.ToShippingRateResponseDto());
                        }

                    default:
                        return NotFound("Invalid carrier.");
                }
            }
            catch (Exception e)
            {
                return e.Message switch
                {
                    "403" => StatusCode(StatusCodes.Status403Forbidden, "Carrier is disabled."),
                    "404" => StatusCode(StatusCodes.Status403Forbidden, "Invalid carrier."),
                    _ => BadRequest("Bad request."),
                };
            }

        }
    }
}
