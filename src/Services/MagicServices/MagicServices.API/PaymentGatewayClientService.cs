using Grpc.Net.Client;
using PaymentGateway.Grpc.Protos;

namespace MagicServices.API
{
    public class PaymentGatewayClientService
    {
        private readonly PaymentGateway.Grpc.Protos.PaymentGateway.PaymentGatewayClient _client;
        public PaymentGatewayClientService()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7219"); 
            _client = new PaymentGateway.Grpc.Protos.PaymentGateway.PaymentGatewayClient(channel);
        }
        public async Task<PaymentResponse> ProcessPaymentAsync(PaymentRequest request)
        {
            return await _client.ProcessPaymentAsync(request);
        }
    }
}
