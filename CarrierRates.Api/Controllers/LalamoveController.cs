using CarrierRates.Api.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarrierRates.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LalamoveController : ControllerBase
    {
        [HttpPost("rates")]
        public IResult PostRates(){
            IEnumerable<RateOption> rateOptions =
            [
                new(){
                    ServiceName = "motorcycle",
                    Price = 20.00m,
                },
                new(){
                    ServiceName = "Car",
                    Price = 40.00m,
                },
            ];

            return Results.Ok(
                new ShippingRateResponseDto()
                {
                    Carrier = "Lalamove",
                    RateOptions = rateOptions
                }
            );
        }
    }
}
