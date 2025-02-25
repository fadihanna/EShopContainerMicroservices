using PaymentGateway.Dto;
using PaymentGateway.Grpc.Protos;

namespace PaymentGateway.Grpc.ClientApi
{
    public interface IPaymentProvider
    {
        Task<PaymentResult> ProcessPayment(PaymentRequest request);
    }
}
