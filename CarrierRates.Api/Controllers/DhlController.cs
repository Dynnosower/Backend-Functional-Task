using System.Diagnostics;
using CarrierRates.Api.Dtos;
using CarrierRates.Api.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace CarrierRates.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DhlController : ControllerBase
    {
        private readonly DhlHttpClient _httpClient;

        public DhlController(DhlHttpClient httpClient){
            _httpClient = httpClient;
        } 

        [Consumes("application/json")]
        [HttpPost("rates")]
        public async Task<IActionResult> PostRates(DhlPostRatesRequestDto dto)
        {
            var queryParams = new Dictionary<string, string>{
                {"accountNumber", dto.AccountNumber.ToString()},
                {"originCountryCode ", dto.OriginCountryCode.ToString()},
                {"originPostalCode ", dto.OriginPostalCode.ToString()},
                {"originCityName ", dto.OriginCityName.ToString()},
                {"destinationCountryCode ", dto.DestinationCountryCode.ToString()},
                {"destinationPostalCode ", dto.DestinationCityName.ToString()},
                {"destinationCityName ", dto.DestinationCityName.ToString()},
                {"weight ", dto.Weight.ToString()},
                {"length ", dto.Length.ToString()},
                {"width ", dto.Width.ToString()},
                {"height ", dto.Height.ToString()},
                {"plannedShippingDate ", dto.PlannedShippingDate.ToString()},
                {"isCustomsDeclarable  ", dto.IsCustomsDeclarable.ToString()},
                {"unitOfMeasurement", dto.UnitOfMeasurement.ToString()},
            };

            Console.WriteLine(queryParams.ToString());
            Debug.WriteLine(queryParams.ToString());

            var response = await _httpClient.PostRates(queryParams);

            return Ok(response);
        }
    }
}
