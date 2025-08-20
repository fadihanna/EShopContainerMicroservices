using Newtonsoft.Json;
using PaymentGateway.Grpc.ClientApi.EbeGateway;
using PaymentGateway.Grpc.Dto.Ebe;
using System.Net.Http.Headers;

public class EbeGatewayService : IEbeGatewayService
{
    private readonly HttpClient _httpClient;

    public EbeGatewayService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PaymentResponseDto> GetPaymentStatusAsync(string checkoutId, string entityId, string baseUrl, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var url = $"{baseUrl}/v3/payments/{checkoutId}?entityId={entityId}";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PaymentResponseDto>(json);
    }

    public async Task<PrepareCheckoutResponseDto> PrepareCheckoutAsync(PrepareCheckoutRequestDto request, string baseUrl, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var formData = new Dictionary<string, string>
        {
            { "entityId", request.EntityId },
            { "entityType", request.EntityType },
            { "merchant.id", request.MerchantId },
            { "checkoutType", request.CheckoutType },
            { "checkoutId", request.CheckoutId },
            { "amount", request.Amount.ToString("F2") },
            { "currency", request.Currency },
            { "paymentType", request.PaymentType }
        };

        var response = await _httpClient.PostAsync($"{baseUrl}/v3/checkouts", new FormUrlEncodedContent(formData));
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PrepareCheckoutResponseDto>(json);
    }

    public async Task<PaymentResponseDto> RefundPaymentAsync(RefundRequestDto request, string baseUrl, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var formData = new Dictionary<string, string>
        {
            { "entityId", request.EntityId },
            { "amount", request.Amount.ToString("F2") },
            { "currency", request.Currency },
            { "paymentType", request.PaymentType },
            { "originalTransactionId", request.OriginalTransactionId },
            { "merchantTransactionId", request.MerchantTransactionId }
        };

        var response = await _httpClient.PostAsync($"{baseUrl}/v3/payments/{request.OriginalTransactionId}", new FormUrlEncodedContent(formData));
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PaymentResponseDto>(json);
    }
}
