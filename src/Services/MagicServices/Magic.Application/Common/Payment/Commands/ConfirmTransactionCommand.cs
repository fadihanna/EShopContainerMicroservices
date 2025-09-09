using BuildingBlocks.Enums;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Models;
using PaymentGateway.Grpc.ClientApi;

namespace Magic.Application.Common.Payment.Commands
{
    public record ConfirmTransactionCommand(int pendingTransactionRequestId, string userId,string checkoutId)
       : ICommand<ConfirmTransactionResponse>;

    public record ConfirmTransactionResponse(PaymentResponseDto paymentResponseDto);

    public class ConfirmTransactionHandler
    : ICommandHandler<ConfirmTransactionCommand, ConfirmTransactionResponse>
    {
        private readonly ITransactionSpecification _transactionSpecification;
        private readonly IPaymentGatewayClientService _paymentGatewayClientService;
        private readonly IDenominationSpecification _denominationSpecification;
        private readonly IRequestSepecification _requestSepecification;
        private readonly IExternalProviderPaymentService _externalProviderPaymentService;
        private readonly IPaymentProvider _paymentProvider;

        public ConfirmTransactionHandler(IRequestSepecification requestSepecification,
            IDenominationSpecification denominationSpecification,
            ITransactionSpecification transactionSpecification,
            IPaymentGatewayClientService paymentGatewayClientService, IExternalProviderPaymentService externalProviderPaymentService, IPaymentProvider paymentProvider)
        {
            _transactionSpecification = transactionSpecification;
            _denominationSpecification = denominationSpecification;
            _requestSepecification = requestSepecification;
            _paymentGatewayClientService = paymentGatewayClientService;
            _externalProviderPaymentService = externalProviderPaymentService;
            _paymentProvider = paymentProvider;

        }
        public async Task<ConfirmTransactionResponse> Handle(ConfirmTransactionCommand command, CancellationToken cancellationToken)
        {

            var getTransactionByRequestId = await _requestSepecification.GetRequest(command.pendingTransactionRequestId, cancellationToken);

            var request = await _requestSepecification.InsertRequestAsync(new Request()
            {
                Amount = Convert.ToDecimal( getTransactionByRequestId.Amount),
                BillingAccount = getTransactionByRequestId?.BillingAccount,
                DenominationId = getTransactionByRequestId.DenominationId,
                RequestDate = DateTime.UtcNow,
                ResponseDate = DateTime.UtcNow,
                Status = Convert.ToInt32(RequestStatus.PaymentConfirm),
                UserId =command.userId
            }, cancellationToken);

            // verify

            // var paymentGatewayResult = await PaymentProcessorVerify(getTransactionByRequestId.ProviderTransactionId,getTransactionByRequestId.PaymentProviderId ,cancellationToken);

            // var paymentGatewayResult = await _paymentProvider.VerifyPayment(getTransactionByRequestId.PaymentProviderTransactionId); // checkoutId
            var paymentGatewayResult = await _paymentProvider.VerifyPayment(command.checkoutId); // checkoutId

            if (paymentGatewayResult.Message.Contains("successfully created checkout"))
                throw new Exception("Payment Not Confirmed");

            //var response = await _transactionSpecification.UpdateAsync(getTransactionByRequestId.Id, cancellationToken);
           
            var paymentResponseDto = new PaymentResponseDto(
                providerTransactionId: getTransactionByRequestId.ProviderTransactionId,
                paymentProviderTransactionId : /*getTransactionByRequestId.PaymentProviderTransactionId*/command.checkoutId,
                transactionId: getTransactionByRequestId.Id.ToString(),
                Status: "Confirmed",
                StatusText: "Transaction Successful",
                TransactionTime: DateTime.UtcNow.ToString(),
                Amount: Convert.ToString(getTransactionByRequestId.Amount),
                Fees: /*Convert.ToString(getTransactionByRequestId.Fees)*/"",
                totalAmount: Convert.ToString(getTransactionByRequestId.Amount),
                billingAccount: getTransactionByRequestId.BillingAccount,
                DetailsList: null,
                requestId : request
            );
            return new ConfirmTransactionResponse(paymentResponseDto);
        }
        private async Task<PaymentGatewayResponseDto> PaymentProcessorVerify(string checkoutId,int paymentProviderId, CancellationToken cancellationToken)
        {
            var paymentRequest = new PaymentGatewayRequestDto(
                Amount: 0,
                Provider: paymentProviderId.ToString(),
                Currency: "EGP",
                checkoutId:checkoutId
            );
            var paymentResponse = await _paymentGatewayClientService.VerifyPaymentAsync(paymentRequest, cancellationToken);

            if (!paymentResponse.Success)
                throw new Exception($"Payment Verification failed: {paymentResponse.Message}");

            return paymentResponse;
        }
    }
}
