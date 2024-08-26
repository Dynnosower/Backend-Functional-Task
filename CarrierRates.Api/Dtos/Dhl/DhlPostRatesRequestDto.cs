using System.ComponentModel;

namespace CarrierRates.Api.Dtos.Dhl;

public record class DhlPostRatesRequestDto
{
    public required string AccountNumber { get; set; }
    public required string OriginCountryCode { get; set; }
    public required string OriginPostalCode { get; set; }
    public required string OriginCityName { get; set; }
    public required string DestinationCountryCode { get; set; }
    public required string DestinationPostalCode { get; set; }
    public required string DestinationCityName { get; set; }
    public required int Weight { get; set; }
    public required int Length { get; set; }
    public required int Width { get; set; }
    public required int Height { get; set; }
    public required string PlannedShippingDate { get; set; }
    public required bool IsCustomsDeclarable { get; set; }
    public required string UnitOfMeasurement { get; set; }
}
