using System.ComponentModel;

namespace CarrierRates.Api.Dtos.Dhl;

public record class DhlPostRatesRequestDto
{
    [DefaultValue("123456789")]
    public required string AccountNumber { get; set; }
    [DefaultValue("CZ")]
    public required string OriginCountryCode { get; set; }
    [DefaultValue("14800")]
    public required string OriginPostalCode { get; set; }
    [DefaultValue("Prague")]
    public required string OriginCityName { get; set; }
    [DefaultValue("CZ")]
    public required string DestinationCountryCode { get; set; }
    [DefaultValue("14800")]
    public required string DestinationPostalCode { get; set; }
    [DefaultValue("Prague")]
    public required string DestinationCityName { get; set; }
    [DefaultValue(5)]
    public required int Weight { get; set; }
    [DefaultValue(15)]
    public required int Length { get; set; }
    [DefaultValue(10)]
    public required int Width { get; set; }
    [DefaultValue(5)]
    public required int Height { get; set; }
    [DefaultValue("2020-02-26")]
    public required string PlannedShippingDate { get; set; }
    [DefaultValue(false)]
    public required bool IsCustomsDeclarable { get; set; }
    [DefaultValue("metric")]
    public required string UnitOfMeasurement { get; set; }
}
