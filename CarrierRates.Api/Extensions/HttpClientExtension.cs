using System;
using System.Net.Http.Headers;
using CarrierRates.Api.HttpClients;

namespace CarrierRates.Api.Extensions;

public static class HttpClientExtension
{
    public static IHttpClientBuilder AddJsonAcceptHeader(this IHttpClientBuilder builder)
    {
        return builder.ConfigureHttpClient(client =>
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });
    }

    public static IServiceCollection AddCustomHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<DhlHttpClient>(client =>
        {
            client.BaseAddress = new Uri("https://api-mock.dhl.com");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        // services.AddHttpClient<DhlHttpClient>()
        // .AddJsonAcceptHeader()
        // .ConfigureHttpClient(client =>
        // {
        //     client.BaseAddress = new Uri("https://api-mock.dhl.com");
        // });

        return services;
    }
}
