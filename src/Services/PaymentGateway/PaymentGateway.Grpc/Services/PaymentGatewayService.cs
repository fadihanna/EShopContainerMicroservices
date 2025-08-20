using Grpc.Core;
using PaymentGateway.Grpc.ClientApi;
using PaymentGateway.Grpc.Protos;

namespace PaymentGateway.Grpc.Services
{
    public class PaymentGatewayService : PaymentGatewayProtoService.PaymentGatewayProtoServiceBase
    {
        private readonly IPaymentProvider _provider;
        public PaymentGatewayService(IPaymentProvider provider)
        {
            _provider = provider;
        }

        public override async Task<PaymentResponse> ProcessPayment(PaymentRequest request, ServerCallContext context)
        {
            var result = await _provider.ProcessPayment(request);

            return new PaymentResponse
            {
                Success = result.Success,
                PaymentprovidertransactionId = result.TransactionId,
                Message = result.Message
            };
        }

        public override async Task<PaymentResponse> VerifyPayment(PaymentRequest request, ServerCallContext context)
        {
            var result = await _provider.VerifyPayment(request.CheckoutId);

            return new PaymentResponse
            {
                Success = result.Success,
                PaymentprovidertransactionId = result.TransactionId,
                Message = result.Message
            };
        }
    }
}
