using System;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using CarrierRates.Api.Dtos.Lalamove;

namespace CarrierRates.Api.HttpClients;

public class LalamoveHttpClient
{
    private readonly HttpClient _httpClient;

    public LalamoveHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
}
