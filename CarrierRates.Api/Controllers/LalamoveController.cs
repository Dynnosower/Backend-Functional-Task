using System.Security.Cryptography;
using System.Text;
using CarrierRates.Api.Dtos;
using CarrierRates.Api.Dtos.Lalamove;
using CarrierRates.Api.HttpClients;
using CarrierRates.Api.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarrierRates.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LalamoveController : ControllerBase
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
            LalamovePostRatesRequestDto requestBody = new()
            {
                Data = new()
                {
                    ServiceType = "MOTORCYCLE",
                    Language = "en_PH",
                    Stops = [
                        new(){
                            Address= "Ilem Street",
                            Coordinates= new(){
                                Lat= "14.394036",
                                Lng= "120.972506",
                            }
                        },
                        new(){
                            Address= "Hizon Street",
                            Coordinates= new(){
                                Lat= "14.398551",
                                Lng= "120.966159",
                            }
                        }
                    ]
                }
            };

            var response = await _httpClient.PostRatesAsync(requestBody);
            return Ok(response.ToShippingRateResponseDto());
        }
    }
}
