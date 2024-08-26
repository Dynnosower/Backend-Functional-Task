using System;
using CarrierRates.Api.Dtos.Dhl;
using Swashbuckle.AspNetCore.Filters;

namespace CarrierRates.Api.Swagger;

public class DhlPostRatesRequestExample : IExamplesProvider<DhlPostRatesRequestDto>
{
    public DhlPostRatesRequestDto GetExamples()
    {
        return new()
        {
            AccountNumber = "123456789",
            OriginCountryCode = "CZ",
            OriginPostalCode = "14800",
            OriginCityName = "Prague",
            DestinationCountryCode = "CZ",
            DestinationPostalCode = "14800",
            DestinationCityName = "Prague",
            Weight = 5,
            Length = 15,
            Width = 10,
            Height = 5,
            PlannedShippingDate = "2020-02-26",
            IsCustomsDeclarable = false,
            UnitOfMeasurement = "metric"
        };
    }
}
