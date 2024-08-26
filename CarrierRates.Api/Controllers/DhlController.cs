using System.Diagnostics;
using System.Text.Json;
using CarrierRates.Api.Dtos.Dhl;
using CarrierRates.Api.HttpClients;
using CarrierRates.Api.Mapping;
using CarrierRates.Api.Swagger;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace CarrierRates.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DhlController : ControllerBase, ICarrierRatesController<DhlPostRatesRequestDto>
    {
        private readonly DhlHttpClient _httpClient;

        public DhlController(DhlHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [Consumes("application/json")]
        [HttpPost("rates")]
        [SwaggerRequestExample(typeof(DhlPostRatesRequestDto), typeof(DhlPostRatesRequestExample))]
        public async Task<IActionResult> PostRates(DhlPostRatesRequestDto request)
        {
            try
            {
                DhlPostRatesResponseDto response = await _httpClient.PostRatesAsync(request);
                return Ok(response.ToShippingRateResponseDto());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
    }
}
