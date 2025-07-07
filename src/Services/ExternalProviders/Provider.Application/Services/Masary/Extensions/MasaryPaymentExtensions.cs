using BuildingBlocks.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Provider.Application.Configuration;
using Provider.Application.Dtos;
using Provider.Application.Services.Masary.Models;

namespace Provider.Application.Services.Masary.Extensions
{
    public static class MasaryPaymentExtensions
    {
        public static MasaryPaymentRequest ToMasaryRequest(this PaymentRequestModel paymentRequestModel, MasarySettings settings, List<ServiceParameterDto> serviceParameters)
        {
            return MasaryFromStandard(paymentRequestModel, settings, serviceParameters);
        }
        private static MasaryPaymentRequest MasaryFromStandard(PaymentRequestModel inquiryRequestModel, MasarySettings settings, List<ServiceParameterDto> serviceParameters)
        {
            var inputParameter = new List<InputParameterList>();

            if (serviceParameters != null && serviceParameters.Count > 0 && inquiryRequestModel.InputParameterList != null && inquiryRequestModel.InputParameterList.Count > 0)
            {
                for (int i = 0; i < serviceParameters.Count; i++)
                {
                    inputParameter.Add(new InputParameterList(
                       Key: serviceParameters[i].Name,
                       Value: inquiryRequestModel.InputParameterList[i].Value
                   ));
                }
            }
            return new MasaryPaymentRequest(
             login: settings.MasaryAccountNumber,
             password: settings.MasaryPassword,
             terminal_id: string.Empty,
             action: settings.TransactionPaymentAction,
             version: settings.Version,
             language: settings.Language,
             MasaryPaymentRequestData: new MasaryPaymentRequestData(
                 InputParameterList: inputParameter,
                 service_version: settings.Version,
                 account_number: settings.MasaryAccountNumber,
                 service_id: int.Parse(inquiryRequestModel.ProviderCode),
                 external_id: inquiryRequestModel.RequestId,
                 amount: inquiryRequestModel.Amount,
                 service_charge: inquiryRequestModel.Fees,
                 total_amount: inquiryRequestModel.Amount + inquiryRequestModel.Fees,
                 inquiry_transaction_id: inquiryRequestModel.InquiryReferenceNumber
         ),
             requestType:"Payment"
     );
        }
        public static PaymentResponseModel MasaryToStandard(this MasaryPaymentResponse masaryPaymentResponse, PaymentRequestModel providerRequest)
        {
            return StandardFromMasary(masaryPaymentResponse, providerRequest);
        }
        private static PaymentResponseModel StandardFromMasary(MasaryPaymentResponse masaryPaymentResponse, PaymentRequestModel providerRequest)
        {
            return new PaymentResponseModel
                    (
                        IsSuccess: masaryPaymentResponse.success,
                        Status: masaryPaymentResponse.data.status,
                        StatusText: masaryPaymentResponse.data.status_text,
                         TransactionTime: masaryPaymentResponse.data.date_time,
                         TransactionId: 1,
                         ProviderTransactionId: masaryPaymentResponse.data.transaction_id,
                         UserId: "1",
                         Amount: providerRequest.Amount.ToString(),
                         Fees: providerRequest.Fees.ToString(),
                         TotalAmount: providerRequest.TotalAmount.ToString(),
                         BillingAccount: string.Empty,
                         DetailsList: masaryPaymentResponse.data?.details_list?.FirstOrDefault()?.Select(d => new ResponseDetail(Key: d.key, Value: d.value)).ToList()
                    );
        }
    }
}
