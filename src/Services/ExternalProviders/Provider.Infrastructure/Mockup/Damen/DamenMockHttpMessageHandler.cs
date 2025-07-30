using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Infrastructure.Mockup.Damen
{
    public class DamenMockHttpMessageHandler : HttpMessageHandler
    {
        private readonly IConfiguration _configuration;

        public DamenMockHttpMessageHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var parentPath = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName;
            string mockupFileName = request.Content.ReadAsStringAsync().Result.Contains("Payment")
                ? "DamenPaymentResponse.json"
                : "DamenInquiryResponse.json";

            string mockupPath = _configuration["ProviderSettings:DamenSettings:MockupPath"];
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
