namespace CarrierRates.Api.Dtos;

public record class ShippingRateResponseDto
{
    public required string Carrier { get; init; }
    public required IEnumerable<RateOption> RateOptions { get; init; }
}

public record class RateOption
{
    public required string ServiceName { get; init; }
    public string EstimatedDelivery { get; init; } = "N/A";
    public required decimal Price { get; init; }
}
