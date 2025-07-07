using BuildingBlocks.Enums;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Models;
using Magic.Application.Common.Interfaces;
using Magic.Application.Dtos.Common;

namespace Magic.Application.Common.Payment.Commands
{
    public record InsertTransactionCommand(PaymentRequestDto Transaction, string userId)
       : ICommand<InsertTransactionResponse>;

    public record InsertTransactionResponse(PaymentResponseDto paymentResponseDto);

    public class InsertTransactionHandler
    : ICommandHandler<InsertTransactionCommand, InsertTransactionResponse>
    {
        private readonly ITransactionSpecification _transactionSpecification;
        private readonly IPaymentGatewayClientService _paymentGatewayClientService;
        private readonly IDenominationSpecification _denominationSpecification;
        private readonly IRequestSepecification _requestSepecification;
        private readonly IExternalProviderPaymentService _externalProviderPaymentService;

        public InsertTransactionHandler(IRequestSepecification requestSepecification,
            IDenominationSpecification denominationSpecification,
            ITransactionSpecification transactionSpecification,
            IPaymentGatewayClientService paymentGatewayClientService, IExternalProviderPaymentService externalProviderPaymentService)
        {
            _transactionSpecification = transactionSpecification;
            _denominationSpecification = denominationSpecification;
            _requestSepecification = requestSepecification;
            _paymentGatewayClientService = paymentGatewayClientService;
            _externalProviderPaymentService = externalProviderPaymentService;

        }
        public async Task<InsertTransactionResponse> Handle(InsertTransactionCommand command, CancellationToken cancellationToken)
        {
            var denomination = await _denominationSpecification.GetByIdAsync(o => o.IsActive && o.Id.Equals(command.Transaction.DenominationId), cancellationToken);
            if (denomination == null)
                throw new NotFoundException("Denomination", command.Transaction.DenominationId);

            var DPC = await _denominationSpecification.GetDenominationProviderCodeByIdAsync(denomination.Id, cancellationToken);

            var request = await _requestSepecification.InsertRequestAsync(new Request()
            {
                Amount = Convert.ToDecimal(command.Transaction.Amount),
                BillingAccount = command.Transaction.BillingAccount,
                DenominationId = command.Transaction.DenominationId,
                RequestDate = DateTime.UtcNow,
                ResponseDate = DateTime.UtcNow,
                Status = Convert.ToInt32(RequestStatus.PaymentInitiate),
                UserId = command.userId
            }, cancellationToken);


            PaymentRequestModel paymentRequestModel = new PaymentRequestModel()
            {
                Amount = Convert.ToDecimal(command.Transaction.Amount),
                ProviderCode = DPC.ProviderCode,
                BillingAccount = command.Transaction.BillingAccount,
                DenominationId = denomination.Id,
                Fees = Convert.ToDecimal(command.Transaction.Fees),
                quantity = command.Transaction.Quantity,
                TotalAmount = Convert.ToDecimal(command.Transaction.Amount) + Convert.ToDecimal(command.Transaction.Fees),
                RequestId = command.Transaction.RequestId,
                PaymentProviderId = command.Transaction.ProviderId,
                InquiryReferenceNumber = command.Transaction.ProviderReferenceNumber,
                UserId = command.userId,
                ProviderTransactionId = "123456789",
                ProviderId = DPC.ProviderId
            };
            // call provider api
            var response = await _externalProviderPaymentService.PaymentAsync(paymentRequestModel, cancellationToken);

            //await PaymentProcessor(command, cancellationToken);

            var transaction = TransactionExtensions.CreateTransaction(paymentRequestModel);
            await _transactionSpecification.InsertAsync(transaction, cancellationToken);

            for (int i = 0; i < command.Transaction.Quantity; i++) // in case of vouchers
            { }
            await _requestSepecification.UpdateRequestStatusAsync(request, Convert.ToInt32(RequestStatus.PaymentSuccess), cancellationToken);

            /*var paymentResponseModel = new PaymentResponseModel
            {
                TransactionId = transaction.Id, // invoiceId
                ProviderTransactionId = "20259238123", // from api
                UserId = "george",
                TotalAmount = paymentRequestModel.TotalAmount,
                Message = "Success",
                Code = "200"
            };*/

            var paymentResponseDto = new PaymentResponseDto(
            providerTransactionId: paymentRequestModel.ProviderTransactionId,
            transactionId: paymentRequestModel.InquiryReferenceNumber,
            Status: "Success",
            StatusText: "Payment completed successfully",
            TransactionTime: DateTime.UtcNow.ToString(),
            Amount: Convert.ToString(command.Transaction.Amount),
            Fees: Convert.ToString(command.Transaction.Fees),
            totalAmount: Convert.ToString(paymentRequestModel.TotalAmount),
            billingAccount: paymentRequestModel.BillingAccount,
            DetailsList: null
 );
            return new InsertTransactionResponse(paymentResponseDto);
        }
        private async Task<PaymentGatewayResponseDto> PaymentProcessor(InsertTransactionCommand command, CancellationToken cancellationToken)
        {
            var paymentRequest = new PaymentGatewayRequestDto(
                Amount: Convert.ToDouble(command.Transaction.Amount) + Convert.ToDouble(command.Transaction.Fees),
                Provider: command.Transaction.ProviderId.ToString(),
                Currency: "EGP"
            );
            var paymentResponse = await _paymentGatewayClientService.ProcessPaymentAsync(paymentRequest, cancellationToken);

            if (!paymentResponse.Success)
                throw new Exception($"Payment failed: {paymentResponse.Message}");

            return paymentResponse;
        }
    }
}
