using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using CarrierRates.Api.Data;
using CarrierRates.Api.Dtos.Dhl;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace CarrierRates.Api.Strategies;

public class DhlStrategy : ICarrierStrategy<DhlPostRatesResponseDto, DhlPostRatesRequestDto>
{
    private readonly HttpClient _httpClient;
    private readonly CarrierRatesContext _dbContext;

    public DhlStrategy(HttpClient httpClient, CarrierRatesContext dbContext)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://api-mock.dhl.com");
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        _dbContext = dbContext;
    }

    public async Task<DhlPostRatesResponseDto> PostRatesAsync(DhlPostRatesRequestDto requestDto)
    {
        var carrier = await _dbContext.Carriers.FirstOrDefaultAsync(carrier => carrier.Name == "DHL");
        
        if (!carrier!.isEnabled)
        {
            throw new Exception(StatusCodes.Status403Forbidden.ToString());
        }
        
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
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<DhlPostRatesResponseDto>(responseString)!;
        }
    }
}
