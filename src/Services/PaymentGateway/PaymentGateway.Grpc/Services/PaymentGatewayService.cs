
using Grpc.Core;
using PaymentGateway.Grpc.ClientApi;
using PaymentGateway.Grpc.ClientApi.Paymob;
using PaymentGateway.Grpc.Protos;

namespace PaymentGateway.Grpc.Services
{
    public class PaymentGatewayService : PaymentGatewayProtoService.PaymentGatewayProtoServiceBase
    {
        private readonly IPayMobService _payMobService;
        public PaymentGatewayService(IPayMobService payMobService)
        {
            _payMobService = payMobService;
        }
        public override async Task<PaymentGateway.Grpc.Protos.PaymentResponse> ProcessPayment(PaymentGateway.Grpc.Protos.PaymentRequest request, ServerCallContext context)
        {
            IPaymentProvider provider = GetPaymentProvider(request.Provider);
            var result = await provider.ProcessPayment(request);

            return new PaymentResponse
            {
                Success = result.Success,
                PaymentprovidertransactionId = result.TransactionId,
                Message = result.Message
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
