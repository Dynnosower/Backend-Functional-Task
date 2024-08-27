namespace CarrierRates.Api.Strategies;

public interface ICarrierStrategy<TResponse, TRequest>
{
    public Task<TResponse> PostRatesAsync(TRequest request);
}
