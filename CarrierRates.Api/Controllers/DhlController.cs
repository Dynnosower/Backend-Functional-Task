using System.Diagnostics;
using System.Text.Json;
using CarrierRates.Api.Dtos.Dhl;
using CarrierRates.Api.HttpClients;
using CarrierRates.Api.Mapping;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> PostRates(DhlPostRatesRequestDto request)
        {
            try
            {
                DhlPostRatesResponseDto response = await _httpClient.PostRatesAsync(request);
                return Ok(response.ToShippingRateResponseDto());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
    }
}
