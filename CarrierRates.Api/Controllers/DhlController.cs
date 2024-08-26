using System.Diagnostics;
using CarrierRates.Api.Dtos.Dhl;
using CarrierRates.Api.HttpClients;
using CarrierRates.Api.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace CarrierRates.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DhlController : ControllerBase
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
            var queryParams = new Dictionary<string, string>{
                {"accountNumber", request.AccountNumber.ToString()},
                {"originCountryCode ", request.OriginCountryCode.ToString()},
                {"originPostalCode ", request.OriginPostalCode.ToString()},
                {"originCityName ", request.OriginCityName.ToString()},
                {"destinationCountryCode ", request.DestinationCountryCode.ToString()},
                {"destinationPostalCode ", request.DestinationCityName.ToString()},
                {"destinationCityName ", request.DestinationCityName.ToString()},
                {"weight ", request.Weight.ToString()},
                {"length ", request.Length.ToString()},
                {"width ", request.Width.ToString()},
                {"height ", request.Height.ToString()},
                {"plannedShippingDate ", request.PlannedShippingDate.ToString()},
                {"isCustomsDeclarable  ", request.IsCustomsDeclarable.ToString()},
                {"unitOfMeasurement", request.UnitOfMeasurement.ToString()},
            };

            Debug.WriteLine(queryParams.ToString());

            var response = await _httpClient.PostRatesAsync(queryParams);

            return Ok(response.ToShippingRateResponseDto());
        }
    }
}
