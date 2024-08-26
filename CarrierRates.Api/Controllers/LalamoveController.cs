using CarrierRates.Api.Dtos.Lalamove;
using CarrierRates.Api.HttpClients;
using CarrierRates.Api.Mapping;
using CarrierRates.Api.Swagger;
using Microsoft.AspNetCore.Mvc;

namespace CarrierRates.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LalamoveController : ControllerBase, ICarrierRatesController<LalamovePostRatesRequestDto>
    {
        private readonly LalamoveHttpClient _httpClient;

        public LalamoveController(LalamoveHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [Consumes("application/json")]
        [HttpPost("rates")]
        public async Task<IActionResult> PostRates(LalamovePostRatesRequestDto request)
        {
            var response = await _httpClient.PostRatesAsync(request);
            return Ok(response.ToShippingRateResponseDto());
        }
    }
}
