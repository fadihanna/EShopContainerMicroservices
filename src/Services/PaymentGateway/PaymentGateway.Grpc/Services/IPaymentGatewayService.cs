using Grpc.Core;
namespace Payment.Service
{
    public interface IPaymentGatewayService
    {
       Task<PaymentGateway.Grpc.PaymentResponse> ProcessPayment(PaymentGateway.Grpc.PaymentRequest request, ServerCallContext context);
    }
}
