using BuildingBlocks.Models;
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
         )
     );
        }
        public static PaymentResponseModel MasaryToStandard(this MasaryPaymentResponse masaryPaymentResponse, PaymentRequestModel providerRequest)
        {
            return StandardFromMasary(masaryPaymentResponse, providerRequest);
        }
        private static PaymentResponseModel StandardFromMasary(MasaryPaymentResponse masaryPaymentResponse, PaymentRequestModel providerRequest)
        {
            return new PaymentResponseModel
            {
                IsSuccess = masaryPaymentResponse.success,
                Status = new Status
                {
                    StatusCode = masaryPaymentResponse.success
                        ? masaryPaymentResponse.PaymentResponseData?.status ?? string.Empty
                        : masaryPaymentResponse.error_code.ToString(),
                    StatusText = masaryPaymentResponse.success
                        ? masaryPaymentResponse.PaymentResponseData?.status_text ?? string.Empty
                        : masaryPaymentResponse.error_text ?? string.Empty
                },
                PaymentResponseData = masaryPaymentResponse.success && masaryPaymentResponse.PaymentResponseData != null
                    ? new BuildingBlocks.Models.PaymentResponseData
                    {
                        TransactionId = masaryPaymentResponse.PaymentResponseData.transaction_id ?? string.Empty,
                        Datetime = masaryPaymentResponse.PaymentResponseData.date_time ?? string.Empty,
                        ResponseCode = masaryPaymentResponse.PaymentResponseData.response_code ?? string.Empty,
                        Amount = providerRequest.Amount,
                        Fees = providerRequest.Fees,
                        TotalAmount = providerRequest.Fees + providerRequest.Amount,
                        paymentResponseDetails = masaryPaymentResponse.PaymentResponseData.paymentResponseDetails?
                            .Select(d => new BuildingBlocks.Models.PaymentResponseDetails
                            {
                                Key = d.key ?? string.Empty,
                                Value = d.value ?? string.Empty
                            }).ToList() ?? new List<BuildingBlocks.Models.PaymentResponseDetails>()
                    }
                    : null
            };
        }
    }
}
