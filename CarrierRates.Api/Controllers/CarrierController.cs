using System.Diagnostics;
using System.Text.Json;
using CarrierRates.Api.Dtos.Dhl;
using CarrierRates.Api.Dtos.Lalamove;
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
