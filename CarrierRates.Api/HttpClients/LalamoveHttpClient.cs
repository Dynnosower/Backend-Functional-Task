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
    public async Task<LalamovePostRatesResponseDto> PostRatesAsync(LalamovePostRatesRequestDto requestBody)
    {
        string secret = "sk_test_f1wK6CbT30CMSuuVMotBnLsx9TBSUXun9y4jFjGYotsvaTONpzAObGtZ+j2G/bbl";
        string key = "pk_test_928e16e2aec2b0cf1abbd99a7d6fef8a";
        string timeStamp = DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalMilliseconds.ToString().Split(".")[0];
        string method = "POST";
        string endpoint = "/v3/quotations";

        string jsonString = JsonSerializer.Serialize(requestBody);

        string rawSignature = $"{timeStamp}\r\n{method}\r\n{endpoint}\r\n\r\n{jsonString}";

        string signature;
        using (var hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
        {
            byte[] signatureBytes = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(rawSignature));
            signature = BitConverter.ToString(signatureBytes).Replace("-", "").ToLower();
        }

        string authorizationHeader = $"hmac {key}:{timeStamp}:{signature}";
        string nonce = Guid.NewGuid().ToString();


        // Set up the HTTP request
        var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
        {
            Content = new StringContent(jsonString, Encoding.UTF8, "application/json")
        };

        Debug.WriteLine("Request: " + jsonString);
        Debug.WriteLine("Authorization: " + authorizationHeader);
        Debug.WriteLine("Request-ID: ", nonce);


        // Add headers
        request.Headers.Add("Authorization", authorizationHeader);
        request.Headers.Add("Market", "PH");
        request.Headers.Add("Request-ID", nonce);

        using (var response = await _httpClient.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var responseJsonString = await response.Content.ReadAsStringAsync();
            // Debug.WriteLine(responseJsonString);
            return JsonSerializer.Deserialize<LalamovePostRatesResponseDto>(responseJsonString)!;
        }
    }
}
