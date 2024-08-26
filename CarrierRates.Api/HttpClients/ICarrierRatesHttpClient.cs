using System;

namespace CarrierRates.Api.HttpClients;

public interface ICarrierRatesHttpClient<TResponse, TRequest>
{
    public Task<TResponse> PostRatesAsync(TRequest request);
}
