using BuildingBlocks.Enums;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Models;
using Magic.Domain.Specifications;
using IPaymentGatewayService = Payment.Service.IPaymentGatewayService;

namespace Magic.Application.Common.Payment.Commands
{
    public record InsertTransactionCommand(PaymentRequestModel Transaction)
       : ICommand<InsertTransactionResponse>;

    public record InsertTransactionResponse(PaymentResponseModel PaymentResponseModel);

    public class InsertTransactionHandler
    : ICommandHandler<InsertTransactionCommand, InsertTransactionResponse>
    {
        private readonly ITransactionSpecification _transactionSpecification;
        private readonly IPaymentGatewayService _paymentGatewayService;
        private readonly IDenominationSpecification _denominationSpecification;
        private readonly IRequestSepecification _requestSepecification;

        public InsertTransactionHandler(IRequestSepecification requestSepecification, IDenominationSpecification denominationSpecification, ITransactionSpecification transactionSpecification, IPaymentGatewayService paymentGatewayService)
        {
            _transactionSpecification = transactionSpecification;
            _paymentGatewayService = paymentGatewayService;
            _denominationSpecification = denominationSpecification;
            _requestSepecification = requestSepecification;
        }
        public async Task<InsertTransactionResponse> Handle(InsertTransactionCommand command, CancellationToken cancellationToken)
        {
            var denomination = await _denominationSpecification.GetByIdAsync(command.Transaction.DenominationId, cancellationToken);
            if (denomination == null)
                throw new NotFoundException("Denomination", command.Transaction.DenominationId);

            var DPC = await _denominationSpecification.GetDenominationProviderCodeByIdAsync(denomination.Id, cancellationToken);

            var request = await _requestSepecification.InsertRequestAsync(new Request()
            {
                Amount = command.Transaction.TotalAmount,
                BillingAccount = command.Transaction.BillingAccount,
                DenominationId = command.Transaction.DenominationId,
                RequestDate = DateTime.UtcNow, //test
                ResponseDate = DateTime.UtcNow, // test
                Status = Convert.ToInt32(RequestStatus.PaymentInitiate),
                UserId = "george" // test,
            }, cancellationToken);

            // call provider api

            await PaymentProcessor(command);

            var transaction = TransactionExtensions.CreateTransaction(command.Transaction);
            await _transactionSpecification.InsertAsync(transaction, cancellationToken);

            for (int i = 0; i < command.Transaction.quantity; i++) // in case of vouchers
            { }
            await _requestSepecification.UpdateRequestStatusAsync(request, Convert.ToInt32(RequestStatus.PaymentSuccess), cancellationToken);

            var paymentResponseModel = new PaymentResponseModel
            {
                TransactionId = transaction.Id, // invoiceId
                ProviderTransactionId = "20259238123", // from api
                UserId = "george",
                TotalAmount = command.Transaction.TotalAmount,
                Message = "Success",
                Code = "200"
            };
            return new InsertTransactionResponse(paymentResponseModel);
        }
        private async Task<PaymentGateway.Grpc.PaymentResponse> PaymentProcessor(InsertTransactionCommand command)
        {
            var paymentRequest = new PaymentGateway.Grpc.PaymentRequest
            {
                Amount = Convert.ToDouble(command.Transaction.TotalAmount),
                Provider = command.Transaction.PaymentProviderId.ToString(),
                Currency = "EGP"
            };
            var paymentResponse = await _paymentGatewayService.ProcessPayment(paymentRequest, null);

            if (!paymentResponse.Success)
                throw new Exception($"Payment failed: {paymentResponse.Message}");
            return paymentResponse;
        }
    }
}
