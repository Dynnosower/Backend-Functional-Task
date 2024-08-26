using System;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using CarrierRates.Api.Dtos.Lalamove;

namespace CarrierRates.Api.Strategies;

public class LalamoveStrategy : ICarrierStrategy<LalamovePostRatesResponseDto, LalamovePostRatesRequestDto>
{
    private readonly HttpClient _httpClient;

    public LalamoveStrategy(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://rest.sandbox.lalamove.com");
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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

        var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
        {
            Content = new StringContent(jsonString, Encoding.UTF8, "application/json")
        };

        request.Headers.Add("Authorization", $"hmac {key}:{timeStamp}:{signature}");
        request.Headers.Add("Market", "PH");
        request.Headers.Add("Request-ID", Guid.NewGuid().ToString());

        using (var response = await _httpClient.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var responseJsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<LalamovePostRatesResponseDto>(responseJsonString)!;
        }
    }
}
