using BuildingBlocks.Models;
using Provider.Grpc.Protos;
using InputParameter = BuildingBlocks.Models.InputParameter;

namespace Provider.Grpc.Extensions
{
    public static class ProviderInquiryServiceExtension
    {
        public static InquiryRequestModel ToStandardRequest(this InquiryRequest inquiryRequestModel)
        {
            return StandardFromGrpc(inquiryRequestModel);
        }
        private static InquiryRequestModel StandardFromGrpc(InquiryRequest request)
        {
            return new InquiryRequestModel(
                InputParameterList: request.InputParameterList
                    .Select(ip => new InputParameter(ip.Key, ip.Value))
                    .ToList(),
                DenominationId: request.DenominationId,
                BillingAccount: request.BillingAccount,
                RequestId: request.RequestId,
                ProviderId: request.ProviderId,
                ProviderCode: request.ProviderCode
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
                Status = responseModel.Status,
                StatusText = responseModel.StatusText,
                DateTime = responseModel.DateTime,
                Amount = responseModel.Amount,
                Fees = responseModel.Fees
            };
        }
    }
}
