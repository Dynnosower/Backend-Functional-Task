using System.Text.Json.Serialization;
using CarrierRates.Api.Dtos.Lalamove.SharedDtos;

namespace CarrierRates.Api.Dtos.Lalamove;

public record class LalamovePostRatesResponseDto
{
    [JsonPropertyName("data")]
    public required DataDto Data { get; set; }
}
