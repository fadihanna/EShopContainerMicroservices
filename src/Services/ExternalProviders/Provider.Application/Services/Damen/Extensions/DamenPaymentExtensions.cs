using BuildingBlocks.Models;
using Provider.Application.Dtos;
using Provider.Application.Services.Damen.Models;
using Provider.Application.Services.Damen.Models.Payment;

namespace Provider.Application.Services.Damen.Extensions
{
    public static class DamenPaymentExtensions
    {
        public static DamenPaymentRequest ToDamenRequest(
            this PaymentRequestModel paymentRequestModel,
            DamenServiceRequestDto mainRequest)
        {
            return DamenFromStandard(paymentRequestModel, mainRequest);
        }

        private static DamenPaymentRequest DamenFromStandard(
            PaymentRequestModel paymentRequestModel,
            DamenServiceRequestDto mainRequest)
        {
            Dictionary<string, string> serviceDataFields = new();

            if (mainRequest?.Inputs != null && paymentRequestModel.InputParameterList != null)
            {
                for (int i = 0; i < mainRequest.Inputs.Count && i < paymentRequestModel.InputParameterList.Count; i++)
                {
                    serviceDataFields[mainRequest.Inputs[i].FieldName] = paymentRequestModel.InputParameterList[i].Value;
                }
            }

            return new DamenPaymentRequest
            {
                ser_id = paymentRequestModel.ProviderCode,
                request_type = mainRequest?.RequestType ?? "payment",
                request_name = mainRequest?.Name ?? "payment_request",
                serviceDataFileds = serviceDataFields,
                inquire_id = paymentRequestModel.InquiryReferenceNumber,
                entity_transaction_id = paymentRequestModel.RequestId
            };
        }

        public static PaymentResponseModel DamenToStandard(this DamenPaymentResponse response, PaymentRequestModel requestModel)
        {
            return StandardFromDamen(response, requestModel);
        }

        private static PaymentResponseModel StandardFromDamen(DamenPaymentResponse response, PaymentRequestModel requestModel)
        {
            var detailsList = response.result?.userServiceDataFileds?
                .Select(kvp => new ResponseDetail(kvp.Key, kvp.Value))
                .ToList();

            return new PaymentResponseModel(
                IsSuccess: response.status.code == "200",
                Status: response.status.code,
                StatusText: response.status.msg?.UserMessageAr ?? response.status.msg?.UserMessage ?? "No message",
                TransactionTime: DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                TransactionId: 1, // Placeholder
                ProviderTransactionId: response.result?.formatedReferance_id ?? response.result?.referance_id,
                UserId: "1", // Placeholder
                Amount: requestModel.Amount.ToString(),
                Fees: requestModel.Fees.ToString(),
                TotalAmount: requestModel.TotalAmount.ToString(),
                BillingAccount: requestModel.BillingAccount,
                DetailsList: detailsList
            );
        }
    }
}
