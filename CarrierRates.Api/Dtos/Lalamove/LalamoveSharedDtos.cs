using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CarrierRates.Api.Dtos.Lalamove.SharedDtos;

public record class DataDto
{
    [JsonPropertyName("serviceType")]
    public required string ServiceType { get; set; }
    [JsonPropertyName("stops")]
    public required List<StopsDto> Stops { get; set; }
    [JsonPropertyName("language")]
    public required string Language { get; set; }
    [JsonPropertyName("priceBreakdown")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PriceBreakDownDto? PriceBreakdown { get; set; }
}


public record class StopsDto
{
    [JsonPropertyName("coordinates")]
    public required CoordinatesDto Coordinates { get; set; }
    [JsonPropertyName("address")]
    public required string Address { get; set; }
}

public record class CoordinatesDto
{
    [JsonPropertyName("lat")]
    public required string Lat { get; set; }
    [JsonPropertyName("lng")]
    public required string Lng { get; set; }
}

public record class PriceBreakDownDto
{
    [JsonPropertyName("total")]
    public required string Total { get; set; }
    [JsonPropertyName("currency")]
    public required string Currency { get; set; }
}

