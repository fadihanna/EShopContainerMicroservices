using Grpc.Core;
using Provider.Application.Common;
using Provider.Grpc.Extensions;
using Provider.Grpc.Protos;

namespace Provider.Grpc.Services
{
    public class ProviderFeesService : ProviderFeesProtoService.ProviderFeesProtoServiceBase
    {
        private readonly ProviderServiceInquiryImplement _providerServiceImplement;

        public ProviderFeesService(ProviderServiceInquiryImplement providerServiceImplement)
        {
            _providerServiceImplement = providerServiceImplement;
        }

        public override async Task<FeesResponse> Fees(FeesRequest request, ServerCallContext context)
        {
            var response = await _providerServiceImplement.GetFees(request.ToStandardRequest());
            return response.ToGrpcResponse();
        }
    }
}
