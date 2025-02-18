using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Provider.Infrastructure.Mockup
{

    public class MockHttpMessageHandler : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var mockResponse = new
            {
                success = true,
                transaction_id = "20604413943744",
                ServiceVersion = 534,
                chargeAmount = 3.0,
                amount = 50.0,
                parameterInput = "01010435825",
                StatusText = "ناجح",
                InfoText = (string?)null,
                inquiry_transaction_id = (string?)null
            };

            var jsonResponse = JsonSerializer.Serialize(mockResponse);
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json")
            };

            return await Task.FromResult(response);
        }
    }
}
