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
        private static MomknPaymentRequest MomknFromStandard(PaymentRequestModel paymentRequestModel)
        {
            return new MomknPaymentRequest();
        }
        public static PaymentResponseModel MomknToStandard(this MomknPaymentResponse momknPaymentResponse)
        {
            return StandardFromMomkn(momknPaymentResponse);
        }
        private static PaymentResponseModel StandardFromMomkn(MomknPaymentResponse momknPaymentResponse)
        {
            // TODO: Implement once Momkn API contract is defined
            return new PaymentResponseModel(
                IsSuccess: false,
                Status: "0",
                StatusText: "payment not yet implemented",
                TransactionTime: string.Empty,
                TransactionId: 0,
                ProviderTransactionId: string.Empty,
                UserId: string.Empty,
                Amount: string.Empty,
                Fees: string.Empty,
                TotalAmount: string.Empty,
                BillingAccount: string.Empty,
                DetailsList: new List<ResponseDetail>()
            );
        }
    }
}
