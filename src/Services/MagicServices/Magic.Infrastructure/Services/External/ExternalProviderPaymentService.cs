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
            PaymentRequest paymentRequestProto = new PaymentRequest
            {
                BillingAccount = request.BillingAccount,
                DenominationId = request.DenominationId,
                RequestId = request.RequestId,
                ProviderCode = request.ProviderCode,
                ProviderId = request.ProviderId,

            };

            paymentRequestProto.InputParameterList.AddRange(
                request.InputParameterList.Select(p => new InputParameterPayment { Key = p.Key, Value = p.Value })
            );

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
    }
}
