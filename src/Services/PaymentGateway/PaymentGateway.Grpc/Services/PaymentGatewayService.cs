using Grpc.Core;
using PaymentGateway.Grpc;
using PaymentGateway.Grpc.Services.PayMob;
using PaymentGateway.Service;

namespace Payment.Service
{
    public class PaymentGatewayService : PaymentGateway.Grpc.PaymentGateway.PaymentGatewayBase, IPaymentGatewayService
    {
        private readonly IPayMobService _payMobService;
        public PaymentGatewayService(IPayMobService payMobService)
        {
            _payMobService = payMobService;
        }
        public override async Task<PaymentGateway.Grpc.PaymentResponse> ProcessPayment(PaymentGateway.Grpc.PaymentRequest request, ServerCallContext context)
        {
            IPaymentProvider provider = GetPaymentProvider(request.Provider);
            var result = await provider.ProcessPayment(request);

            return new PaymentResponse
            {
                Success = true,
                PaymentprovidertransactionId = result.TransactionId,
                Message = "Success"
            };
        }
        private IPaymentProvider GetPaymentProvider(string providerId)
        {
            return providerId switch
            {
                "1" => new PaymobPaymentProvider(_payMobService),
                _ => throw new NotSupportedException($"Provider '{providerId}' is not supported.")
            };
        }
    }
}