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

public class DhlHttpClient : ICarrierRatesHttpClient
{
    private readonly HttpClient _httpClient;

    public DhlHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> PostRatesAsync(object request)
    {
        var requestDto = (DhlPostRatesRequestDto)request;

        var queryParams = new Dictionary<string, string>{
                {"accountNumber", requestDto.AccountNumber.ToString()},
                {"originCountryCode ", requestDto.OriginCountryCode.ToString()},
                {"originPostalCode ", requestDto.OriginPostalCode.ToString()},
                {"originCityName ", requestDto.OriginCityName.ToString()},
                {"destinationCountryCode ", requestDto.DestinationCountryCode.ToString()},
                {"destinationPostalCode ", requestDto.DestinationCityName.ToString()},
                {"destinationCityName ", requestDto.DestinationCityName.ToString()},
                {"weight", requestDto.Weight.ToString()},
                {"length", requestDto.Length.ToString()},
                {"width", requestDto.Width.ToString()},
                {"height", requestDto.Height.ToString()},
                {"plannedShippingDate", requestDto.PlannedShippingDate.ToString()},
                {"isCustomsDeclarable", requestDto.IsCustomsDeclarable.ToString()},
                {"unitOfMeasurement", requestDto.UnitOfMeasurement.ToString()},
            };

        string endpoint = "/mydhlapi/rates";
        var requestUri = QueryHelpers.AddQueryString(endpoint, queryParams!);
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUri);

        httpRequest.Headers.Add("Message-Reference", "d0e7832e-5c98-11ea-bc55-0242ac13");
        httpRequest.Headers.Add("Message-Reference-Date", "Wed, 21 Oct 2015 07:28:00 GMT");
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic", "ZGVtby1rZXk6ZGVtby1zZWNyZXQ=");

        using (var response = await _httpClient.SendAsync(httpRequest))
        {
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
