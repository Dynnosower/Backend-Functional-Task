using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using CarrierRates.Api.Dtos;
using Microsoft.AspNetCore.WebUtilities;

namespace CarrierRates.Api.HttpClients;

public class DhlHttpClient
{
    private readonly HttpClient _httpClient;

    public DhlHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<DhlPostRatesResponseDto> PostRates(Dictionary<string, string> queryParams)
    {
        string endpoint = "/mydhlapi/rates";
        var requestUri = QueryHelpers.AddQueryString(endpoint, queryParams!);
        var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

        request.Headers.Add("Message-Reference", "d0e7832e-5c98-11ea-bc55-0242ac13");
        request.Headers.Add("Message-Reference-Date", "Wed, 21 Oct 2015 07:28:00 GMT");
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", "ZGVtby1rZXk6ZGVtby1zZWNyZXQ=");

        using (var response = await _httpClient.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(jsonString);
            return JsonSerializer.Deserialize<DhlPostRatesResponseDto>(jsonString)!;
        }
    }
}
