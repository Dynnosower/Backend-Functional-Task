using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using CarrierRates.Api.Extensions;
using CarrierRates.Api.HttpClients;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCustomHttpClients();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend Developer Functional Task", Version = "v1" });
});

builder.Services.AddLogging(config =>
{
    config.AddDebug();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

app.Run();


