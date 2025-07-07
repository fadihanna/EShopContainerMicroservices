using Grpc.Core;
using Provider.Application.Common;
using Provider.Grpc.Protos;
using Provider.Grpc.Extensions;
namespace Provider.Grpc.Services
{
    public class ProviderPaymentService : ProviderPaymentProtoService.ProviderPaymentProtoServiceBase
    {
        private readonly ProviderServiceInquiryImplement _providerServiceImplement;

        public ProviderPaymentService(ProviderServiceInquiryImplement providerServiceImplement)
        {
            _providerServiceImplement = providerServiceImplement;
        }

        public override async Task<PaymentResponse> Payment(PaymentRequest request, ServerCallContext context)
        {
            var response = await _providerServiceImplement.Payment(request.ToStandardRequest());
            return Provider.Grpc.Extensions.ProviderPaymentServiceExtension.ToGrpcResponse(response);

        }
    }
}
