using System;
using System.Net.Http.Headers;
using CarrierRates.Api.HttpClients;

namespace CarrierRates.Api.Extensions;

public static class HttpClientExtension
{
    public static IServiceCollection AddCustomHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<DhlHttpClient>(client =>
        {
            client.BaseAddress = new Uri("https://api-mock.dhl.com");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        services.AddHttpClient<LalamoveHttpClient>(client =>
        {
            client.BaseAddress = new Uri("https://rest.sandbox.lalamove.com");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        return services;
    }
}
