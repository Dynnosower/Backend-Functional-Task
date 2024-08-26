using System;
using CarrierRates.Api.Swagger;
using Swashbuckle.AspNetCore.Filters;

namespace CarrierRates.Api.Extensions;

public static class SwaggerExampleExtensions
{
    public static IServiceCollection AddCustomSwaggerExamples(this IServiceCollection services){
        services.AddSwaggerExamplesFromAssemblyOf<LalamovePostRatesRequestExample>();
        services.AddSwaggerExamplesFromAssemblyOf<DhlPostRatesRequestExample>();
        return services;
    }
}
