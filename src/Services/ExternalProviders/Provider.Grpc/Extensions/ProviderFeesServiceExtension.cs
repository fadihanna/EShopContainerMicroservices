using BuildingBlocks.Models;
using Provider.Grpc.Protos;

namespace Provider.Grpc.Extensions
{
    public static class ProviderFeesServiceExtension
    {
        public static FeesRequestModel ToStandardRequest(this FeesRequest request)
        {
            return new FeesRequestModel(
                ProviderCode: request.ProviderCode,   
                Amount: request.Amount,   
                ProviderId: request.ProviderId,
                RequestId: request.RequestId
            );
        }
        public static FeesResponse ToGrpcResponse(this FeesResponseModel feesResponseModel)
        {
            return StandardToGrpc(feesResponseModel);
        }
        private static FeesResponse StandardToGrpc(FeesResponseModel responseModel)
        {
            return new FeesResponse
            {
                Status = responseModel.Status,
                StatusText = responseModel.StatusText,
                DateTime = responseModel.DateTime,
                Amount = responseModel.Amount,
                Fees = responseModel.Fees,
                ProviderReferenceNumber = responseModel.ProviderReferenceNumber
             };
        }
    }
}
