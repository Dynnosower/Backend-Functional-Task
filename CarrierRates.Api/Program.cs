using System.Net.Http.Headers;
using CarrierRates.Api.Factories;
using CarrierRates.Api.HttpClients;
using CarrierRates.Api.Strategies;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<DhlHttpClient>(client =>
{
    client.BaseAddress = new Uri("https://api-mock.dhl.com");
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json")
    );
});

builder.Services.AddHttpClient<LalamoveHttpClient>(client =>
{
    client.BaseAddress = new Uri("https://rest.sandbox.lalamove.com");
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json")
    );
});

builder.Services.AddScoped<CarrierStrategyFactory>();
builder.Services.AddScoped<LalamoveStrategy>();
builder.Services.AddScoped<DhlStrategy>();

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo { Title = "Backend Developer Functional Task", Version = "v1" }
    );
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
