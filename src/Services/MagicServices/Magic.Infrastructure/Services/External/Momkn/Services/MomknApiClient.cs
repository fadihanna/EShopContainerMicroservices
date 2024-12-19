using Magic.Infrastructure.Services.External.Momkn.Models;
using System.Text;

namespace Magic.Infrastructure.Services.External.Momkn.Services
{
    public class MomknApiClient
    {
        private readonly HttpClient _httpClient;
        public MomknApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MomknInquiryResponse> SendInquiryRequestAsync(MomknInquiryRequest providerRequest)
        {
            var json = JsonSerializer.Serialize(providerRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://masary-api-url", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Momkn API request failed");
            }
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<MomknInquiryResponse>(result);
        }

        public async Task<MomknPaymentResponse> SendPaymentRequestAsync(MomknPaymentRequest providerRequest)
        {
            throw new NotImplementedException();
        }
    }
}
