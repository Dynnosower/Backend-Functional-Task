namespace CarrierRates.Api.Dtos;

public record class RateOption
{
    public required string ServiceName { get; init; }
    public DateTime EstimatedDelivery { get; init; }
    public required decimal Price { get; init; }
}
