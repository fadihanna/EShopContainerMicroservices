using Provider.Application.Services.Masary;
using Provider.Application.Services.Masary.Models;
using System.Text;
using System.Text.Json;

namespace Provider.Infrastructure.Services.External.Masary.Services;

public class MasaryApiClient : IMasaryApiClient
{
    private readonly HttpClient _httpClient;

    public MasaryApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<MasaryInquiryResponse> SendInquiryRequestAsync(MasaryInquiryRequest providerRequest, string URL)
    {
        return await SendRequestAsync<MasaryInquiryRequest, MasaryInquiryResponse>(providerRequest, URL, "Inquiry");
    }

    public async Task<MasaryPaymentResponse> SendPaymentRequestAsync(MasaryPaymentRequest providerRequest, string URL)
    {
        return await SendRequestAsync<MasaryPaymentRequest, MasaryPaymentResponse>(providerRequest, URL, "Payment");
    }

    private async Task<TResponse> SendRequestAsync<TRequest, TResponse>(TRequest providerRequest, string URL, string requestType)
    {
        var json = JsonSerializer.Serialize(providerRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

        var response = await _httpClient.PostAsync(URL, content, cts.Token);
        var result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }
        return JsonSerializer.Deserialize<TResponse>(result) ?? throw new Exception($"Deserialization failed.");
    }
}