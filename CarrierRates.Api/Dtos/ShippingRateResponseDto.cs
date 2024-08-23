namespace CarrierRates.Api.Dtos;

public record class ShippingRateResponseDto
{
    public required string Carrier { get; init; }
    public required IEnumerable<RateOption> RateOptions { get; init; }
}
