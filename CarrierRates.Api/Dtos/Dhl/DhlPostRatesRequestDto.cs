using System.Text.Json.Serialization;

namespace CarrierRates.Api.Dtos.Dhl;

public record class DhlPostRatesRequestDto
{
    [JsonPropertyName("accountNumber")]
    public required string AccountNumber { get; set; }
    [JsonPropertyName("originCountryCode")]
    public required string OriginCountryCode { get; set; }
    [JsonPropertyName("originPostalCode")]
    public required string OriginPostalCode { get; set; }
    [JsonPropertyName("originCityName")]
    public required string OriginCityName { get; set; }
    [JsonPropertyName("destinationCountryCode")]
    public required string DestinationCountryCode { get; set; }
    [JsonPropertyName("destinationPostalCode")]
    public required string DestinationPostalCode { get; set; }
    [JsonPropertyName("destinationCityName")]
    public required string DestinationCityName { get; set; }
    [JsonPropertyName("weight")]
    public required int Weight { get; set; }
    [JsonPropertyName("length")]
    public required int Length { get; set; }
    [JsonPropertyName("width")]
    public required int Width { get; set; }
    [JsonPropertyName("height")]
    public required int Height { get; set; }
    [JsonPropertyName("plannedShippingDate")]
    public required string PlannedShippingDate { get; set; }
    [JsonPropertyName("isCustomsDeclarable")]
    public required bool IsCustomsDeclarable { get; set; }
    [JsonPropertyName("unitOfMeasurement")]
    public required string UnitOfMeasurement { get; set; }
}
