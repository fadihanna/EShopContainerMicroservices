using BuildingBlocks.Models;
using Provider.Grpc.Protos;

namespace Provider.Grpc.Extensions
{
    public static class ProviderPaymentServiceExtension
    {
        public static PaymentRequestModel ToStandardRequest(this PaymentRequest request)
        {
            return new PaymentRequestModel()
            {
                Fees = Convert.ToDecimal(request.Fees),
                Amount = Convert.ToDecimal(request.Amount),
                ProviderId = request.ProviderId,
                RequestId = request.RequestId,
                BillingAccount = request.BillingAccount,
                DenominationId = request.DenominationId,
                PaymentProviderId = request.PaymentProviderId,
                ProviderCode = request.ProviderCode,
                InquiryReferenceNumber = request.InquiryReferenceNumber,
                quantity = request.Quantity,
                TotalAmount = Convert.ToDecimal(request.TotalAmount),
                ProviderTransactionId = request.ProviderTransactionId,
            };
        }
        public static PaymentResponse ToGrpcResponse(this PaymentResponseModel paymentResponseModel)
        {
            return StandardToGrpc(paymentResponseModel);
        }
        private static PaymentResponse StandardToGrpc(PaymentResponseModel responseModel)
        {
            try
            {
                return new PaymentResponse
                {
                    Status = Convert.ToString(responseModel.Status),
                    ProviderTransactionId = "1",
                    TotalAmount = Convert.ToString(responseModel.TotalAmount),
                    Amount = responseModel.TotalAmount,
                    BillingAccount = responseModel.BillingAccount,
                    Fees = responseModel.Fees,
                    StatusText = responseModel.Status,
                    TransactionId = responseModel.TransactionId.ToString(),
                    TransactionTime = responseModel.TransactionTime,
                    DetailsList = {
                    responseModel.DetailsList?.Select(p =>
                        new DetailsPayment { Key = p.Key, Value = p.Value }
                    ) ?? Enumerable.Empty<DetailsPayment>()
                }
                };
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
