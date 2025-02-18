using PaymentGateway.DTO;
using PaymentGateway.Grpc;

namespace PaymentGateway.Service
{
    public interface IPaymentProvider
    {
        Task<PaymentResult> ProcessPayment(PaymentRequest request);
    }
}
