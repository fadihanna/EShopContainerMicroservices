using BuildingBlocks.Models;
using Provider.Application.Services.Masary.Models;

namespace Provider.Application.Services.Masary.Extensions
{
    public static class MasaryPaymentExtensions
    {
        public static MasaryPaymentRequest ToMasaryRequest(this PaymentRequestModel paymentRequestModel)
        {
            return MasaryFromStandard(paymentRequestModel);
        }
        private static MasaryPaymentRequest MasaryFromStandard(PaymentRequestModel inquiryRequestModel)
        {
            return new MasaryPaymentRequest(
                     login: string.Empty,
                     password: string.Empty,
                     terminal_id: string.Empty,
                     action: string.Empty,
                      version: 1,
                     language: string.Empty,
                     MasaryPaymentRequestData:
                     new MasaryPaymentRequestData(InputParameterList: new List<InputParameterList>(),
                            service_version: 0,
                            account_number: string.Empty,
                            service_id: 0,
                            external_id: string.Empty,
                            amount: 0,
                            service_charge: 0,
                            total_amount: 0,
                            inquiry_transaction_id: inquiryRequestModel.RefrenceTransactionId)
            );
        }
        public static PaymentResponseModel MasaryToStandard(this MasaryPaymentResponse masaryPaymentResponse)
        {
            return StandardFromMasary(masaryPaymentResponse);
        }
        private static PaymentResponseModel StandardFromMasary(MasaryPaymentResponse masaryInquiryResponse)
        {
            //TODO
            return new PaymentResponseModel();
        }
    }
}
