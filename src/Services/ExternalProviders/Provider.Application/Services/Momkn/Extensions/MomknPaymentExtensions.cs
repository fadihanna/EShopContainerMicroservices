using BuildingBlocks.Models;
using Provider.Application.Services.Momkn.Models;

namespace Provider.Application.Services.Momkn.Extensions
{
    public static class MomknPaymentExtensions
    {
        public static MomknPaymentRequest ToMomknRequest(this PaymentRequestModel paymentRequestModel)
        {
            return MomknFromStandard(paymentRequestModel);
        }
        private static MomknPaymentRequest MomknFromStandard(PaymentRequestModel inquiryRequestModel)
        {
            return new MomknPaymentRequest();
        }
        public static PaymentResponseModel MomknToStandard(this MomknPaymentResponse momknPaymentResponse)
        {
            return StandardFromMomkn(momknPaymentResponse);
        }
        private static PaymentResponseModel StandardFromMomkn(MomknPaymentResponse momknInquiryResponse)
        {
            //TODO
            return new PaymentResponseModel();
        }
    }
}
