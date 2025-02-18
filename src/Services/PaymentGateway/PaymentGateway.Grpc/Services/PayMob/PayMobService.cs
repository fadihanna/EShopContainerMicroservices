using PaymentGateway.DTO.Login;
using PaymentGateway.DTO.Order.Request;
using PaymentGateway.DTO.Order.Response;
using PaymentGateway.DTO.Payment;
using Newtonsoft.Json;
using System.Text;
using MagicPaymentAPI.DTO;
using Microsoft.Extensions.Options;

namespace PaymentGateway.Grpc.Services.PayMob
{
    public class PayMobService : IPayMobService
    {
        private readonly string _baseURL;
        private readonly string _apiKey;
        public PayMobService(IOptions<AppSettings> appSettings)
        {
            _baseURL = "https://accept.paymob.com/api/"; //appSettings.Value.PayMobUrl;
            _apiKey = "ZXlKaGJHY2lPaUpJVXpVeE1pSXNJblI1Y0NJNklrcFhWQ0o5LmV5SmpiR0Z6Y3lJNklrMWxjbU5vWVc1MElpd2libUZ0WlNJNkltbHVhWFJwWVd3aUxDSndjbTltYVd4bFgzQnJJam8zTURNMk5qQjkudS1vRUQxTWo3VF9EZm9Tbl9ObWxOd3U0VXRzaFV6YXJrVlBsUGo0Ul9JRjlQUWNiNE9OeFoxVENzU2owbGQtNHJjd05FZjVLWW5ieGVnLXlNQ3d6UUE=ZXlKaGJHY2lPaUpJVXpVeE1pSXNJblI1Y0NJNklrcFhWQ0o5LmV5SmpiR0Z6Y3lJNklrMWxjbU5vWVc1MElpd2libUZ0WlNJNkltbHVhWFJwWVd3aUxDSndjbTltYVd4bFgzQnJJam8zTURNMk5qQjkudS1vRUQxTWo3VF9EZm9Tbl9ObWxOd3U0VXRzaFV6YXJrVlBsUGo0Ul9JRjlQUWNiNE9OeFoxVENzU2owbGQtNHJjd05FZjVLWW5ieGVnLXlNQ3d6UUE=";
            //appSettings.Value.PayMobApiKey;
        }
        public async Task<PaymobLoginResponse> Login()
        {
            string url = _baseURL + "auth/tokens";
            var payload = new { api_key = _apiKey };
            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(60);
            var data = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, data);
            string result = response.Content.ReadAsStringAsync().Result;
            var objectResult = JsonConvert.DeserializeObject<PaymobLoginResponse>(result);
            return objectResult;
        }
        public async Task<PaymobCreateOrderResponse> CreateOrder(PaymobCreateOrderRequest createOrderRequest)
        {
            string url = _baseURL + "ecommerce/orders";
            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(60);
            var data = new StringContent(JsonConvert.SerializeObject(createOrderRequest), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, data);
            string result = response.Content.ReadAsStringAsync().Result; // extra description is null
            var objectResult = JsonConvert.DeserializeObject<PaymobCreateOrderResponse>(result);
            return objectResult;
        }
        public async Task<PaymobPaymentResponse> Payment(PaymobPaymentRequest paymentRequest)
        {
            string url = _baseURL + "acceptance/payment_keys";
            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(60);
            var data = new StringContent(JsonConvert.SerializeObject(paymentRequest), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, data);
            string result = response.Content.ReadAsStringAsync().Result;
            var objectResult = JsonConvert.DeserializeObject<PaymobPaymentResponse>(result);
            return objectResult;
        }
    }
}
