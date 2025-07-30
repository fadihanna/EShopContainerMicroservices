using Provider.Application.Configuration;
using Provider.Application.Services.Damen;
using Provider.Application.Services.Damen.Models;
using Provider.Application.Services.Damen.Models.Payment;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Provider.Infrastructure.Services.External.Damen.Services;

public class DamenApiClient : IDamenApiClient
{
    private readonly HttpClient _httpClient;

    public DamenApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<DamenPaymentResponse> SendPaymentRequestAsync(DamenPaymentRequest request, DamenSettings damenSettings )
    {
        return await SendRequestAsync<DamenPaymentRequest, DamenPaymentResponse>(request, damenSettings, "Payment");
    }

    public async Task<DamenInquiryResponse> SendInquiryRequestAsync(DamenInquiryRequest request, DamenSettings damenSettings)
    {
        return await SendRequestAsync<DamenInquiryRequest, DamenInquiryResponse>(request, damenSettings, "Inquiry");
    }

    private async Task<TResponse> SendRequestAsync<TRequest, TResponse>(
        TRequest request,
        DamenSettings damenSettings,
        string requestType)
    {
         string requestJson = JsonSerializer.Serialize(request);
        var data = new StringContent(requestJson, Encoding.UTF8, "application/json");

         var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
        };

        using var client = new HttpClient(handler)
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

         string requestHash = GenerateHMACMD5(requestJson, damenSettings.DamenKey);

         client.DefaultRequestHeaders.Add("client-id", damenSettings.DamenClientID);
        client.DefaultRequestHeaders.Add("request-hash", requestHash);
        client.DefaultRequestHeaders.Add("app-version", damenSettings.DamenAppVersion);

         var requestUrl = $"{damenSettings.DamenBaseURL}" + "transaction/execute";

         var response = await client.PostAsync(requestUrl, data);
        string result = await response.Content.ReadAsStringAsync();

         if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Damen {requestType} failed with status code: {response.StatusCode}, Response: {result}");

         return JsonSerializer.Deserialize<TResponse>(result)
               ?? throw new Exception($"Failed to deserialize Damen {requestType} response.");
    }
    private static string GenerateHMACMD5(string message, string key)
    {
        using var hmac = new HMACMD5(Encoding.UTF8.GetBytes(key));
        var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }

}
