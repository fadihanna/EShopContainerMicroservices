using BuildingBlocks.Enums;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Models;
using Microsoft.AspNetCore.Authentication;
using PaymentGateway.Grpc.ClientApi;
using PaymentGateway.Grpc.ClientApi.EbeGateway;

namespace Magic.Application.Common.Payment.Commands
{
    public record InsertTransactionCommand(PaymentRequestDto Transaction,string userId)
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
        private readonly IPaymentProvider _paymentProvider;
        private readonly IAuthenticationService _authenticationService;
        public InsertTransactionHandler(IRequestSepecification requestSepecification,
            IDenominationSpecification denominationSpecification,
            ITransactionSpecification transactionSpecification,
            IPaymentGatewayClientService paymentGatewayClientService, IExternalProviderPaymentService externalProviderPaymentService, IPaymentProvider paymentProvider,IAuthenticationService authenticationService)
        {
            _transactionSpecification = transactionSpecification;
            _denominationSpecification = denominationSpecification;
            _requestSepecification = requestSepecification;
            _paymentGatewayClientService = paymentGatewayClientService;
            _externalProviderPaymentService = externalProviderPaymentService;
            _paymentProvider = paymentProvider;
            _authenticationService = authenticationService;
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
                UserId =command.userId 
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
                RequestId = Convert.ToString(request),
                PaymentProviderId = 3, // ebe
                InquiryReferenceNumber = command.Transaction.ProviderReferenceNumber,
                UserId = command.userId,
                ProviderId = DPC.ProviderId,
                InputParameterList = command.Transaction.InputParameterList,
            };

            // call provider api
            var response = await _externalProviderPaymentService.PaymentAsync(paymentRequestModel, cancellationToken);

            // var paymentGatewayResult = await PaymentProcessor(command, cancellationToken);

            //  create checkout
            // var paymentGatewayResult = await PaymentProcessorInitiate(command, cancellationToken);
            
            /*var paymentGatewayResult = await _paymentProvider.ProcessPayment(new PaymentGateway.Grpc.Protos.PaymentRequest() { Amount = Convert.ToDouble(paymentRequestModel.Amount),Currency = "EGP",Provider = paymentRequestModel.ProviderId.ToString(),CheckoutId = "0"});
            paymentRequestModel.PaymentProviderTransactionId = paymentGatewayResult.TransactionId;*/

            paymentRequestModel.ProviderTransactionId = response.ProviderTransactionId;
            paymentRequestModel.PaymentProviderTransactionId = command.Transaction.checkoutId;

            // Create pending transaction record
            var transaction = TransactionExtensions.CreateTransaction(paymentRequestModel);
            transaction.Status = Convert.ToInt32(RequestStatus.PaymentSuccess);

            await _transactionSpecification.InsertAsync(transaction, cancellationToken);

            // Return checkoutId to frontend (so iframe can be loaded)
            var paymentResponseDto = new PaymentResponseDto(
                providerTransactionId: response.ProviderTransactionId,
                //paymentProviderTransactionId : paymentGatewayResult.TransactionId,
                paymentProviderTransactionId: command.Transaction.checkoutId,
                transactionId: transaction.Id.ToString(),
                Status: transaction.Status.ToString(),
                StatusText: "Payment Successful",
                TransactionTime: DateTime.UtcNow.ToString(),
                Amount: Convert.ToString(command.Transaction.Amount),
                Fees: Convert.ToString(command.Transaction.Fees),
                totalAmount: Convert.ToString(paymentRequestModel.TotalAmount),
                billingAccount: paymentRequestModel.BillingAccount,
                DetailsList: null,
                requestId : request
            );
            return new InsertTransactionResponse(paymentResponseDto);
/*


            var transaction = TransactionExtensions.CreateTransaction(paymentRequestModel);

            try
            {

            await _transactionSpecification.InsertAsync(transaction, cancellationToken);

            }
            catch (Exception ex)
            {

            await _requestSepecification.UpdateRequestStatusAsync(request, Convert.ToInt32(RequestStatus.PaymentSuccess), cancellationToken);

            */    
        }
        private async Task<PaymentGatewayResponseDto> PaymentProcessorInitiate(InsertTransactionCommand command, CancellationToken cancellationToken)
        {
            var paymentRequest = new PaymentGatewayRequestDto(
                Amount: Convert.ToDouble(command.Transaction.Amount) + Convert.ToDouble(command.Transaction.Fees),
                Provider: "3", // ebe
                Currency: "EGP",
                checkoutId : ""
            );
            var paymentResponse = await _paymentGatewayClientService.ProcessPaymentAsync(paymentRequest, cancellationToken);

            if (!paymentResponse.Success)
                throw new Exception($"Payment failed: {paymentResponse.Message}");

            return paymentResponse;
        }
    }
}
