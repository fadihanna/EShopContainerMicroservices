using BuildingBlocks.Models;
using Provider.Grpc.Protos;
using Details = Provider.Grpc.Protos.Details;
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
                DetailsList = { responseModel.DetailsList.Select(d => new Details { Key = d.Key, Value = d.Value }) }
            };
        }
    }
}
