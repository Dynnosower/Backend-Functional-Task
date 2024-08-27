using System;
using Microsoft.EntityFrameworkCore;

namespace CarrierRates.Api.Data;

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app){
        using var scope = app.Services.CreateScope();
        var DbContext = scope.ServiceProvider.GetRequiredService<CarrierRatesContext>();
        await DbContext.Database.MigrateAsync();
    }
}
