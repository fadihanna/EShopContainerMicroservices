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

    public async Task<MasaryInquiryResponse> SendInquiryRequestAsync(MasaryInquiryRequest providerRequest)
    {
        var json = JsonSerializer.Serialize(providerRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("https://masary-api-url", content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Masary API request failed");
        }
        var result = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<MasaryInquiryResponse>(result);
    }

    public async Task<MasaryPaymentResponse> SendPaymentRequestAsync(MasaryPaymentRequest providerRequest)
    {
        var json = JsonSerializer.Serialize(providerRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("https://masary-api-url", content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Masary API request failed");
        }
        var result = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<MasaryPaymentResponse>(result);
    }
}
