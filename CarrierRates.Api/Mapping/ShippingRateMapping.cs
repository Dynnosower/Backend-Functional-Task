using CarrierRates.Api.Dtos;
using CarrierRates.Api.Dtos.Lalamove;
using CarrierRates.Api.Dtos.Dhl;

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
                EstimatedDelivery = DateOnly.Parse(product.DeliveryCapabilities.EstimatedDeliveryDateAndTime).ToString(),
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

    public static ShippingRateResponseDto ToShippingRateResponseDto(this LalamovePostRatesResponseDto dto)
    {
        List<RateOption> rateOptions = [];
        rateOptions.Add(new RateOption()
        {
            ServiceName = "LALAMOVE " + dto.Data.ServiceType,
            Price = Decimal.Parse(dto.Data.PriceBreakdown!.Total)
        });

        return new(){
            Carrier = "Lalamove",
            RateOptions = rateOptions
        };
    }
}
