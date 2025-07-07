using Microsoft.Extensions.Configuration;
using System.Net;
using System.Text;

namespace Provider.Infrastructure.Mockup
{

    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly IConfiguration _configuration;

        public MockHttpMessageHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var parentPath = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName;
            string mockupFileName = request.Content.ReadAsStringAsync().Result.Contains("Payment") ? "MasaryPaymentResponse.json" : "MasaryInquiryResponse.json";
            string mockupPath = _configuration["ProviderSettings:MasarySettings:MockupInquiryResponsePath"];

            string fileName = Path.Combine(parentPath, mockupPath, mockupFileName);

            string result = File.ReadAllText(fileName);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(result, Encoding.UTF8, "application/json")
            };

            return await Task.FromResult(response);
        }
    }
}
