using PaymentGateway.Grpc.ClientApi;

namespace PaymentGateway.Grpc.Factories
{
    public interface IPaymentProviderFactory
    {
        IPaymentProvider GetProvider(string providerId);
    }
}
