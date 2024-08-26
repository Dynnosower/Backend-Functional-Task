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
    public class DhlController : ControllerBase, ICarrierRatesController
    {
        private readonly DhlHttpClient _httpClient;

        public DhlController(DhlHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [Consumes("application/json")]
        [HttpPost("rates")]
        public async Task<IActionResult> PostRates(object request)
        {
            if (request is not DhlPostRatesRequestDto requestDto)
            {
                return BadRequest("Bad request.");
            }

            try
            {
                var responseString = await _httpClient.PostRatesAsync(requestDto);
                DhlPostRatesResponseDto responseDto = JsonSerializer.Deserialize<DhlPostRatesResponseDto>(responseString)!;
                return Ok(responseDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
    }
}
