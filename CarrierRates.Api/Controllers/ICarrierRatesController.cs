using System;
using CarrierRates.Api.Dtos.Dhl;
using Microsoft.AspNetCore.Mvc;

namespace CarrierRates.Api.Controllers;

public interface ICarrierRatesController
{
    public Task<IActionResult> PostRates(object requestDto);
}
