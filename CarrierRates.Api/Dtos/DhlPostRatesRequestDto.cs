using System.ComponentModel;

namespace CarrierRates.Api.Dtos;

public record class DhlPostRatesRequestDto
{
    [DefaultValue("123456789")]
    public string AccountNumber { get; set; } = "123456789";
    [DefaultValue("CZ")]
    public string OriginCountryCode { get; set; } = "CZ";
    [DefaultValue("14800")]
    public string OriginPostalCode { get; set; } = "14800";
    [DefaultValue("Prague")]
    public string OriginCityName { get; set; } = "Prague";
    [DefaultValue("CZ")]
    public string DestinationCountryCode { get; set; } = "CZ";
    [DefaultValue("14800")]
    public string DestinationPostalCode { get; set; } = "14800";
    [DefaultValue("Prague")]
    public string DestinationCityName { get; set; } = "Prague";
    [DefaultValue(5)]
    public int Weight { get; set; } = 5;
    [DefaultValue(15)]
    public int Length { get; set; } = 15;
    [DefaultValue(10)]
    public int Width { get; set; } = 10;
    [DefaultValue(5)]
    public int Height { get; set; } = 5;
    [DefaultValue("2020-02-26")]
    public string PlannedShippingDate { get; set; } = "2020-02-26";
    [DefaultValue(false)]
    public bool IsCustomsDeclarable { get; set; } = false;
    [DefaultValue("metric")]
    public string UnitOfMeasurement { get; set; } = "metric";
}
