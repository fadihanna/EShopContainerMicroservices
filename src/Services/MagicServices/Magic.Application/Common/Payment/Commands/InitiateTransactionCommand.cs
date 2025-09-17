using BuildingBlocks.Enums;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Models;
using Microsoft.AspNetCore.Authentication;
using PaymentGateway.Grpc.ClientApi;
using PaymentGateway.Grpc.ClientApi.EbeGateway;

namespace Magic.Application.Common.Payment.Commands
{
    public record InitiateTransactionCommand(PaymentRequestDto Transaction, string userId)
       : ICommand<InitiateTransactionResponse>;

    public record InitiateTransactionResponse(PaymentResponseDto paymentResponseDto);
    public class InitiateTransactionHandler
    : ICommandHandler<InitiateTransactionCommand, InitiateTransactionResponse>
    {
        private readonly ITransactionSpecification _transactionSpecification;
        private readonly IPaymentGatewayClientService _paymentGatewayClientService;
        private readonly IDenominationSpecification _denominationSpecification;
        private readonly IRequestSepecification _requestSepecification;
        private readonly IExternalProviderPaymentService _externalProviderPaymentService;
        private readonly IPaymentProvider _paymentProvider;
        private readonly IAuthenticationService _authenticationService;
        public InitiateTransactionHandler(IRequestSepecification requestSepecification,
            IDenominationSpecification denominationSpecification,
            ITransactionSpecification transactionSpecification,
            IPaymentGatewayClientService paymentGatewayClientService, IExternalProviderPaymentService externalProviderPaymentService, IPaymentProvider paymentProvider, IAuthenticationService authenticationService)
        {
            _transactionSpecification = transactionSpecification;
            _denominationSpecification = denominationSpecification;
            _requestSepecification = requestSepecification;
            _paymentGatewayClientService = paymentGatewayClientService;
            _externalProviderPaymentService = externalProviderPaymentService;
            _paymentProvider = paymentProvider;
            _authenticationService = authenticationService;
        }
        public async Task<InitiateTransactionResponse> Handle(InitiateTransactionCommand command, CancellationToken cancellationToken)
        {
            var denomination = await _denominationSpecification.GetByIdAsync(o => o.IsActive && o.Id.Equals(command.Transaction.DenominationId), cancellationToken);
            if (denomination == null)
                throw new NotFoundException("Denomination", command.Transaction.DenominationId);

            var DPC = await _denominationSpecification.GetDenominationProviderCodeByIdAsync(denomination.Id, cancellationToken);

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
                PaymentProviderId = 3, // ebe
                InquiryReferenceNumber = command.Transaction.ProviderReferenceNumber,
                UserId = command.userId,
                ProviderId = DPC.ProviderId,
                InputParameterList = command.Transaction.InputParameterList,
            };
            var paymentGatewayResult = await _paymentProvider.ProcessPayment(new PaymentGateway.Grpc.Protos.PaymentRequest() { Amount = Convert.ToDouble(paymentRequestModel.TotalAmount), Currency = "EGP", Provider = paymentRequestModel.ProviderId.ToString(), CheckoutId = "0" });

            //paymentRequestModel.PaymentProviderTransactionId = paymentGatewayResult.TransactionId;

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


            var paymentResponseDto = new PaymentResponseDto(
                providerTransactionId: "",
                paymentProviderTransactionId: paymentGatewayResult.TransactionId,
                transactionId: paymentGatewayResult.TransactionId,
                Status: Convert.ToString(RequestStatus.PaymentInitiate),
                StatusText: "Payment Checkout Screen Initiated",
                TransactionTime: DateTime.UtcNow.ToString(),
                Amount: Convert.ToString(command.Transaction.Amount),
                Fees: Convert.ToString(command.Transaction.Fees),
                totalAmount: Convert.ToString(paymentRequestModel.TotalAmount),
                billingAccount: paymentRequestModel.BillingAccount,
                DetailsList: null,
                requestId: request
            );
            return new InitiateTransactionResponse(paymentResponseDto);
        }
        private async Task<PaymentGatewayResponseDto> PaymentProcessorInitiate(InsertTransactionCommand command, CancellationToken cancellationToken)
        {
            var paymentRequest = new PaymentGatewayRequestDto(
                Amount: Convert.ToDouble(command.Transaction.Amount) + Convert.ToDouble(command.Transaction.Fees),
                Provider: "3", // ebe
                Currency: "EGP",
                checkoutId: ""
            );
            var paymentResponse = await _paymentGatewayClientService.ProcessPaymentAsync(paymentRequest, cancellationToken);

            if (!paymentResponse.Success)
                throw new Exception($"Payment failed: {paymentResponse.Message}");

            return paymentResponse;
        }
    }
}
