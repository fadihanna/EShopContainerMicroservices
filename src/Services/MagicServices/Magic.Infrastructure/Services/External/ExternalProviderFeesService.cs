using BuildingBlocks.Models;
using Provider.Grpc.Protos;

namespace Magic.Infrastructure.Services.External
{
    public class ExternalProviderFeesService : IExternalProviderFeesService
    {
        private readonly ProviderFeesProtoService.ProviderFeesProtoServiceClient _providerFeesProto;

        public ExternalProviderFeesService(ProviderFeesProtoService.ProviderFeesProtoServiceClient providerFeesProto)
        {
            _providerFeesProto = providerFeesProto;
        }

        public async Task<FeesResponseModel> FeesAsync(FeesRequestModel request, CancellationToken cancellationToken)
        {
            FeesRequest feesRequestProto = new FeesRequest
            {
                RequestId = request.RequestId,
                ProviderCode = request.ProviderCode,
                Amount = request.Amount,
                ProviderId = request.ProviderId
            };

            var response = await _providerFeesProto.FeesAsync(feesRequestProto);

            // Map the response to FeesResponseModel
            return new FeesResponseModel(
                ProviderReferenceNumber: response.ProviderReferenceNumber,
                Status: response.Status,
                StatusText: response.StatusText,
                DateTime: response.DateTime,
                Fees: response.Fees,
                Amount: response.Amount,
                TotalAmount: response.Amount + response.Fees
           );
        }
    }
}
