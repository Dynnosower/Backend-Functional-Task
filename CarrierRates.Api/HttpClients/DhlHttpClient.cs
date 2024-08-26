using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using CarrierRates.Api.Dtos.Dhl;
using CarrierRates.Api.Mapping;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace CarrierRates.Api.HttpClients;

public class DhlHttpClient
{
    private readonly HttpClient _httpClient;

    public DhlHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
}
