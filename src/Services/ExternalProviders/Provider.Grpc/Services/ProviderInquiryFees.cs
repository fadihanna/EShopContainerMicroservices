using BuildingBlocks.Models;
using Grpc.Core;
using Provider.Application.Common;
using Provider.Grpc.Protos;
using Provider.Grpc.Extensions;  

namespace Provider.Grpc.Services
{
    public class ProviderInquiryFees : Provider.Grpc.Protos.ProviderInquiryFees.ProviderInquiryFeesBase
    {
        private readonly ProviderServiceInquiryImplement _providerServiceImplement;

        public ProviderInquiryFees(ProviderServiceInquiryImplement providerServiceImplement)
        {
            _providerServiceImplement = providerServiceImplement;
        }

        public override async Task<InquiryFeesResponse> InquiryFees(InquiryFeesRequest request, ServerCallContext context)
        {
            var response = await _providerServiceImplement.GetFees(request.ToStandardRequest());
            return response.ToGrpcResponse();
        }
    }
}
