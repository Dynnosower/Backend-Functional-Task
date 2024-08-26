using System;
using CarrierRates.Api.Dtos.Lalamove;
using Swashbuckle.AspNetCore.Filters;

namespace CarrierRates.Api.Swagger;

public class LalamovePostRatesRequestExample : IExamplesProvider<LalamovePostRatesRequestDto>
{
    public LalamovePostRatesRequestDto GetExamples()
    {
        return new()
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
    }
}
