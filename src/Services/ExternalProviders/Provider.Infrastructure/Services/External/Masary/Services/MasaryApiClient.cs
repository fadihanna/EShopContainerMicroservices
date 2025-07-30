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

        //var response = await _httpClient.PostAsync(URL, content, cts.Token);
        //var result = await response.Content.ReadAsStringAsync();

        /*if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }*/
        var result = @"{
           ""success"": true,
           ""language"": ""ar"",
           ""action"": ""TransactionInquiry"",
           ""version"": 2,
           ""data"": {
             ""transaction_id"": ""406414792908"",
             ""status"": 2,
             ""status_text"": ""ناجح"",
             ""date_time"": ""05/03/2023 14:43:45"",
             ""info_text"": ""Billing Account: 03100058253\nClient Name: ساره احمد سعدالدين محمود\nDue Date: 2023-04-02\nالقيمة المستحقة للفاتورة 1267"",
             ""amount"": 1267.0,
             ""min_amount"": 1.0,
             ""max_amount"": 100000.0
           }}";
        return JsonSerializer.Deserialize<TResponse>(result) ?? throw new Exception($"Deserialization failed.");
    }
}