using ApplicationModels = Magic.Application.Dtos.Common;
using BuildingBlocksModels = BuildingBlocks.Models;

namespace Magic.Application.Extensions
{
    public static class InquiryServiceExtension
    {
        public static InquiryRequestModel ToStandardRequest(this InquiryRequestDto inquiryRequestDto, string providerCode, int providerId)
        {
            return new InquiryRequestModel(
                InputParameterList: inquiryRequestDto.InputParameterList.Select(p => new BuildingBlocksModels.InputParameter(Key: p.Key, Value: p.Value)).ToList(),
                BillingAccount: inquiryRequestDto.BillingAccount,
                DenominationId: inquiryRequestDto.DenominationId,
                RequestId: string.Empty,
                ProviderCode: providerCode,
                ProviderId: providerId
            );
        }
        public static InquiryResponseDto ToModelResponse(this InquiryResponseModel inquiryResponseDto)
        {
            return new InquiryResponseDto
            (
                TransactionId: inquiryResponseDto.TransactionId,
                Status: inquiryResponseDto.Status,
                StatusText: inquiryResponseDto.StatusText,
                DateTime: inquiryResponseDto.DateTime,
                Amount: inquiryResponseDto.Amount,
                Fees: inquiryResponseDto.Fees,
                DetailsList: inquiryResponseDto.DetailsList.Select(p => new ApplicationModels.Details(Key: p.Key, Value: p.Value)).ToList()
            );
        }
    }
}
