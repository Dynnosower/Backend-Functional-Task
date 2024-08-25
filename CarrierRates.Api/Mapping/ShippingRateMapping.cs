using System;
using System.Runtime.Serialization;
using CarrierRates.Api.Dtos;

namespace CarrierRates.Api.Mapping;

public static class ShippingRateMapping
{
    public static ShippingRateResponseDto ToShippingRateResponseDto(this DhlPostRatesResponseDto dto)
    {
        List<RateOption> rateOptions = [];
        foreach (var product in dto.Products)
        {
            var rateOption = new RateOption()
            {
                ServiceName = product.ProductName,
                EstimatedDelivery = DateOnly.Parse(product.DeliveryCapabilities.EstimatedDeliveryDateAndTime),
                Price = product.TotalPrice[0].Price
            };
            rateOptions.Add(rateOption);
        }
        return new()
        {
            Carrier = "DHL",
            RateOptions = rateOptions
        };
    }
}
