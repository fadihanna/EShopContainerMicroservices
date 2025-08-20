/*using PaymentGateway.Grpc.ClientApi;
using PaymentGateway.Grpc.ClientApi.EbeGateway;
using System;
using System.Collections.Generic;

namespace PaymentGateway.Grpc.Factories
{
    public class PaymentProviderFactory : IPaymentProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, Type> _providerMap;

        public PaymentProviderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _providerMap = new Dictionary<string, Type>
            {
                { "3", typeof(EbePaymentProvider) }
                // { "1", typeof(PaymobPaymentProvider) } // later
            };
        }

        public IPaymentProvider GetProvider(string providerId)
        {
            if (!_providerMap.TryGetValue(providerId, out var providerType))
                throw new NotSupportedException($"Provider '{providerId}' is not supported.");

            return (IPaymentProvider)_serviceProvider.GetRequiredService(providerType);
        }
    }
}
*/