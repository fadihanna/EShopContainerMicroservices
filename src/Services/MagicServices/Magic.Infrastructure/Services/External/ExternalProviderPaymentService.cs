using BuildingBlocks.Models;
using Provider.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Details = BuildingBlocks.Models.Details;
using InputParameter = Provider.Grpc.Protos.InputParameter;

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

            PaymentResponseData paymentResponseData = new PaymentResponseData()
            {
                Fees = decimal.Parse(response.Fees),
                Amount = decimal.Parse(response.Amount),
                ResponseCode = response.Status,
                TotalAmount = decimal.Parse(response.TotalAmount)
            };
            var paymentResponseModel = new PaymentResponseModel
            {
                ProviderTransactionId = response.TransactionId,
                PaymentResponseData = paymentResponseData
            };

            return paymentResponseModel;
        }
    }
}
