using BuildingBlocks.Models;
using Provider.Grpc.Protos;

namespace Provider.Grpc.Extensions
{
    public static class ProviderInquiryFeesExtension
    {
        public static FeesRequestModel ToStandardRequest(this InquiryFeesRequest request)
        {
            return new FeesRequestModel(
                DenomiantionId: request.DenominationId,   
                Amount: request.Amount,   
                ProviderId: request.ProviderId,
                BillerCode: request.BillerCode
            );
        }
        public static InquiryResponse ToGrpcResponse(this InquiryResponseModel inquiryResponseModel)
        {
            return StandardToGrpc(inquiryResponseModel);
        }
        private static InquiryResponse StandardToGrpc(InquiryResponseModel responseModel)
        {
            return new InquiryResponse
            {
                TransactionId = responseModel.TransactionId,
                Status = responseModel.Status,
                StatusText = responseModel.StatusText,
                DateTime = responseModel.DateTime,
                Amount = responseModel.Amount,
                Fees = responseModel.Fees
             };
        }
    }
}
