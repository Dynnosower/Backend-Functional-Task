using System.Text.Json.Serialization;

namespace CarrierRates.Api.Dtos;

public record class DhlPostRatesResponseDto
{
    [JsonPropertyName("products")]
    public required List<ProductDto> Products { get; init; }
}

public record class ProductDto
{
    [JsonPropertyName("productName")]
    public required string ProductName { get; init; }
    [JsonPropertyName("deliveryCapabilities")]
    public required DeliveryCapabilitiesDto DeliveryCapabilities { get; init; }
    [JsonPropertyName("totalPrice")]
    public required List<PriceDto> TotalPrice { get; init; }
}

public record class DeliveryCapabilitiesDto
{
    [JsonPropertyName("estimatedDeliveryDateAndTime")]
    public required string EstimatedDeliveryDateAndTime { get; init; }
}

public record class PriceDto
{
    [JsonPropertyName("currencyType")]
    public required string CurrencyType { get; init; }
    [JsonPropertyName("priceCurrency")]
    public required string PriceCurrency { get; init; }
    [JsonPropertyName("price")]
    public required decimal Price { get; init; }
}
