using BuildingBlocks.Models;
using Provider.Grpc.Protos;

namespace Magic.Infrastructure.Services.External
{
    public class ExternalProviderPaymentService : IExternalProviderPaymentService
    {
        private readonly ProviderPaymentProtoService.ProviderPaymentProtoServiceClient _providerPaymentProto;

        public ExternalProviderPaymentService(ProviderPaymentProtoService.ProviderPaymentProtoServiceClient providerPaymentProto)
        {
            _providerPaymentProto = providerPaymentProto;
        }
        public async Task<PaymentResponseModel> PaymentAsync(PaymentRequestModel request, CancellationToken cancellationToken)
        { 

            try
            {
                PaymentRequest paymentRequestProto = new PaymentRequest
                {
                    BillingAccount = request.BillingAccount,
                    DenominationId = request.DenominationId,
                    RequestId = request.RequestId,
                    ProviderCode = request.ProviderCode,
                    ProviderId = request.ProviderId,
                    Fees = 1,
                    Amount = 20,
                    TotalAmount = 30,
                    Quantity = 1,
                    PaymentProviderId = 1,
                    ProviderTransactionId = "202323",
                    InquiryReferenceNumber = "2313"
                };

                paymentRequestProto.InputParameterList.AddRange(
                    request.InputParameterList.Select(p => new InputParameterPayment { Key = p.Key, Value = p.Value })
                );

                var xx = 0;
                var response = await _providerPaymentProto.PaymentAsync(paymentRequestProto);

                return new PaymentResponseModel
                        (
                            IsSuccess: response.Status == "2",
                            Status: response.Status,
                            StatusText: response.StatusText,
                             TransactionTime: response.TransactionTime,
                             TransactionId: 1,
                             ProviderTransactionId: response.ProviderTransactionId,
                             UserId: "1",
                             Amount: response.Amount,
                             Fees: response.Fees,
                             TotalAmount: response.TotalAmount,
                             BillingAccount: string.Empty,
                             DetailsList: response.DetailsList?.Select(d => new ResponseDetail(Key: d.Key, Value: d.Value)).ToList()
                        );
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
