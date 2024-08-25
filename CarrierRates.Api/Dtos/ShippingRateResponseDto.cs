namespace CarrierRates.Api.Dtos;

public record class ShippingRateResponseDto
{
    public required string Carrier { get; init; }
    public required IEnumerable<RateOption> RateOptions { get; init; }
}

public record class RateOption
{
    public required string ServiceName { get; init; }
    public DateOnly EstimatedDelivery { get; init; }
    public required decimal Price { get; init; }
}
