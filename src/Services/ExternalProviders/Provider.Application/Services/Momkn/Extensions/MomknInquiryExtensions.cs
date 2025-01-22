using BuildingBlocks.Models;
using Provider.Application.Services.Momkn.Models;

namespace Provider.Application.Services.Momkn.Extensions
{
    public static class MomknInquiryExtensions
    {
        public static MomknInquiryRequest ToMomknRequest(this InquiryRequestModel inquiryRequestModel)
        {
            return MomknFromStandard(inquiryRequestModel);
        }
        private static MomknInquiryRequest MomknFromStandard(InquiryRequestModel inquiryRequestModel)
        {
            return new MomknInquiryRequest();
        }
        public static InquiryResponseModel MomknToStandard(this MomknInquiryResponse momknInquiryResponse)
        {
            return StandardFromMomkn(momknInquiryResponse);
        }
        private static InquiryResponseModel StandardFromMomkn(MomknInquiryResponse momknInquiryResponse)
        {
            return new InquiryResponseModel(
                TransactionId: string.Empty,
                Status: string.Empty,
                StatusText: string.Empty,
                DateTime: string.Empty,
                DetailsList: new List<Details>()
            );
        }
    }
}
