using CarrierRates.Api.Strategies;

namespace CarrierRates.Api.Factories;

public class CarrierStrategyFactory
{
    private readonly IServiceProvider _serviceProvider;

    public CarrierStrategyFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public object CreateStrategy(string carrierType)
    {
        return carrierType switch
        {
            "DHL" => _serviceProvider.GetRequiredService<DhlStrategy>(),
            "Lalamove" => _serviceProvider.GetRequiredService<LalamoveStrategy>(),
            _ => throw new Exception("Invalid carrier type"),
        };
    }
}
