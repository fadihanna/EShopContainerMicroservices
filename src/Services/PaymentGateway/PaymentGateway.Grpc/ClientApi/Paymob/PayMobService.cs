/*using MagicPaymentAPI.DTO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PaymentGateway.Dto.Login;
using PaymentGateway.Dto.Order.Request;
using PaymentGateway.Dto.Order.Response;
using PaymentGateway.Dto.Payment;
using System.Text;

namespace PaymentGateway.Grpc.ClientApi.Paymob
{
    public class PayMobService : IPayMobService
    {
        private readonly string _baseURL;
        private readonly string _apiKey;
        private readonly IHttpClientFactory _httpClientFactory;

        public PayMobService(IOptions<AppSettings> appSettings, IHttpClientFactory httpClientFactory)
        {
            _baseURL = appSettings.Value.PayMobUrl;
            _apiKey = appSettings.Value.PayMobApiKey;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PaymobLoginResponse> Login()
        {
            string url = _baseURL + "auth/tokens";
            var payload = new { api_key = _apiKey };
            var client = _httpClientFactory.CreateClient("paymob");
            client.Timeout = TimeSpan.FromSeconds(180);
            var data = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, data);
            string result = await response.Content.ReadAsStringAsync();
            var objectResult = JsonConvert.DeserializeObject<PaymobLoginResponse>(result);
            return objectResult;
        }
        public async Task<PaymobCreateOrderResponse> CreateOrder(PaymobCreateOrderRequest createOrderRequest)
        {
            string url = _baseURL + "ecommerce/orders"; 
            var client = _httpClientFactory.CreateClient("paymob");
            client.Timeout = TimeSpan.FromSeconds(180);
            var data = new StringContent(JsonConvert.SerializeObject(createOrderRequest), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, data);
            string result = await response.Content.ReadAsStringAsync();
            var objectResult = JsonConvert.DeserializeObject<PaymobCreateOrderResponse>(result);
            return objectResult;
        }

        public async Task<PaymobPaymentResponse> Payment(PaymobPaymentRequest paymentRequest)
        {
            string url = _baseURL + "acceptance/payment_keys";
            var client = _httpClientFactory.CreateClient("paymob");
            client.Timeout = TimeSpan.FromSeconds(180);
            var data = new StringContent(JsonConvert.SerializeObject(paymentRequest), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, data);
            string result = await response.Content.ReadAsStringAsync();
            var objectResult = JsonConvert.DeserializeObject<PaymobPaymentResponse>(result);
            return objectResult;
        }
    }
}
*/