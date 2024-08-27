using System;

namespace CarrierRates.Api.Entities;

public class Carrier
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public bool isEnabled { get; set; }
    public DateOnly UpdatedAt { get; set; }
}
