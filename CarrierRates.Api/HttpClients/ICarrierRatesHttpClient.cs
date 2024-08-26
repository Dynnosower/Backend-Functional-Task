using System;

namespace CarrierRates.Api.HttpClients;

public interface ICarrierRatesHttpClient
{
    public Task<string> PostRatesAsync(object request);
}
