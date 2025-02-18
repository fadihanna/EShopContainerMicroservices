using Grpc.Core;
using Provider.Application.Common;
using Provider.Grpc.Extensions;
using Provider.Grpc.Protos;

namespace Provider.Grpc.Services
{
    public class ProviderInquiryService : ProviderInquiryProtoService.ProviderInquiryProtoServiceBase
    {
        private readonly ProviderServiceInquiryImplement _providerServiceImplement;
        public ProviderInquiryService(ProviderServiceInquiryImplement providerServiceImplement)
        {
            _providerServiceImplement = providerServiceImplement;
        }
        public override async Task<InquiryResponse> Inquiry(InquiryRequest request, ServerCallContext context)
        {
            var response = await _providerServiceImplement.Inquiry(request.ToStandardRequest());
            return response.ToGrpcResponse();
        }
    }
}