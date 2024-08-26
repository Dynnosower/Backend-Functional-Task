using System.Text.Json;
using CarrierRates.Api.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCustomHttpClients();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend Developer Functional Task", Version = "v1" });
    c.ExampleFilters();
});
builder.Services.AddCustomSwaggerExamples();

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


