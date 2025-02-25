using BuildingBlocks.Enums;
using BuildingBlocks.Exceptions;

namespace Magic.Application.Common.Payment.Commands
{
    public record InsertTransactionCommand(PaymentRequestModel Transaction)
       : ICommand<InsertTransactionResponse>;

    public record InsertTransactionResponse(PaymentResponseModel PaymentResponseModel);

    public class InsertTransactionHandler
    : ICommandHandler<InsertTransactionCommand, InsertTransactionResponse>
    {
        private readonly ITransactionSpecification _transactionSpecification;
        private readonly IPaymentGatewayClientService _paymentGatewayClientService;
        private readonly IDenominationSpecification _denominationSpecification;
        private readonly IRequestSepecification _requestSepecification;

        public InsertTransactionHandler(IRequestSepecification requestSepecification,
            IDenominationSpecification denominationSpecification,
            ITransactionSpecification transactionSpecification,
            IPaymentGatewayClientService paymentGatewayClientService)
        {
            _transactionSpecification = transactionSpecification;
            _denominationSpecification = denominationSpecification;
            _requestSepecification = requestSepecification;
            _paymentGatewayClientService = paymentGatewayClientService;
        }
        public async Task<InsertTransactionResponse> Handle(InsertTransactionCommand command, CancellationToken cancellationToken)
        {
            var denomination = await _denominationSpecification.GetByIdAsync(o => o.IsActive && o.Id.Equals(command.Transaction.DenominationId), cancellationToken);
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

            await PaymentProcessor(command, cancellationToken);

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
        private async Task<PaymentGatewayResponseDto> PaymentProcessor(InsertTransactionCommand command, CancellationToken cancellationToken)
        {
            var paymentRequest = new PaymentGatewayRequestDto(
                Amount: Convert.ToDouble(command.Transaction.TotalAmount),
                Provider: command.Transaction.PaymentProviderId.ToString(),
                Currency: "EGP"
            );
            var paymentResponse = await _paymentGatewayClientService.ProcessPaymentAsync(paymentRequest, cancellationToken);

            if (!paymentResponse.Success)
                throw new Exception($"Payment failed: {paymentResponse.Message}");

            return paymentResponse;
        }
    }
}
