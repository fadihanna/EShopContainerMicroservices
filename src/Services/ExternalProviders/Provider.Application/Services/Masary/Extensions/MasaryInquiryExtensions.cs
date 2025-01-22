using BuildingBlocks.Models;
using Provider.Application.Services.Masary.Models;

namespace Provider.Application.Services.Masary.Extensions
{
    public static class MasaryInquiryExtensions
    {

        public static MasaryInquiryRequest ToMasaryRequest(this InquiryRequestModel inquiryRequestModel)
        {
            return MasaryFromStandard(inquiryRequestModel);
        }
        private static MasaryInquiryRequest MasaryFromStandard(InquiryRequestModel inquiryRequestModel)
        {
            return new MasaryInquiryRequest(
                Lang:string.Empty,
                ServiceId: inquiryRequestModel.DenominationId,
                Amount:0,
                Quantity:0,
                ParameterInput:string.Empty,
                ExternalId: inquiryRequestModel.RequestId,
                IsPayAfterInquire:false
            );
        }
        public static InquiryResponseModel MasaryToStandard(this MasaryInquiryResponse masaryInquiryResponse)
        {
            return StandardFromMasary(masaryInquiryResponse);
        }
        private static InquiryResponseModel StandardFromMasary(MasaryInquiryResponse masaryInquiryResponse)
        {
            return new InquiryResponseModel(
                TransactionId: masaryInquiryResponse.InquiryResponseDetails.transaction_id,
                Status: masaryInquiryResponse.InquiryResponseDetails.status,
                StatusText: masaryInquiryResponse.InquiryResponseDetails.status_text,
                DateTime: masaryInquiryResponse.InquiryResponseDetails.date_time,
                DetailsList: new List<Details>()
            );
        }
    }
}
