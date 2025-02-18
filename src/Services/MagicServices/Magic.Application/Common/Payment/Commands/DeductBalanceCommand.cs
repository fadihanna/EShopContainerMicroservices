/*using Magic.Application.Common.Interfaces;
using Magic.Domain.Specifications;

namespace Magic.Application.Common.Payment.Commands
{
    public record DeductBalanceCommand(PaymentRequestDto Request) 
                : ICommand<PaymentResponseDto>;
    //decimal amount,
    //decimal fees,
    //string brn,
    public record PaymentResponseDto(
                string providerTransactionId,
                string invoiceId,
                string  code,
                string message,
                DateTime transactionTime,
                string totalAmount,
                string billingAccount,
                int denominationId,
                string userId
                //List<Details> DetailsList

        );
    public class BalanceCommandHandler : ICommandHandler<DeductBalanceCommand, PaymentResponseDto>
    {
        private readonly IDenominationSpecification _denominationSpecification;
        private readonly IBalanceSpecification _balanceSpecification;
        private readonly ILookUpSpecification _lookUpSpecification;
        private readonly IExternalProviderInquiryService _externalProviderInquiryService;
        private readonly ILanguageService _languageService;
        private readonly ITransactionSpecification _transactionSpecification;
        public BalanceCommandHandler(ITransactionSpecification transactionSpecification,IBalanceSpecification balanceSpecification,IExternalProviderInquiryService externalProviderInquiryService, IDenominationSpecification denominationSpecification, ILookUpSpecification lookUpSpecification, ILanguageService languageService)
        {
            _externalProviderInquiryService = externalProviderInquiryService;
            _denominationSpecification = denominationSpecification;
            _lookUpSpecification = lookUpSpecification;
            _languageService = languageService;
            _balanceSpecification = balanceSpecification;
            _transactionSpecification = transactionSpecification;
        }
        public async Task<PaymentResponseDto> Handle(DeductBalanceCommand request, CancellationToken cancellationToken)
        {
            // make here that center balance is deducted in case not  a user
            // make another handler for transaction to add transaction to transaction table
            
            var denomination = await _denominationSpecification.GetDenominationProviderCodeByIdAsync(request.Request.denominationId, cancellationToken);

            if (denomination.IsNullResult)
                throw new InquiryResponseException((int)DomainEnums.InternalErrorCode.EntityNotFound, _lookUpSpecification, _languageService.GetLanguage());

            // var balance = await _balanceSpecification.GetByUserId(request.Request.UserId, cancellationToken);

            // if (balance == null)
            // throw new InquiryResponseException((int)DomainEnums.InternalErrorCode.EntityNotFound, _lookUpSpecification, _languageService.GetLanguage());
            // convert request to balance dto
            //var balance = CreateBalance(request.Request);
            //var deduct = await _balanceSpecification.DeductAsync(balance, cancellationToken);

            return new PaymentResponseDto(

                providerTransactionId: "",
                invoiceId: "2923801",
                code: "200",
                message: "Success",
                transactionTime: DateTime.UtcNow,
                totalAmount: "100",
                billingAccount: "01229411164",
                denominationId: 1,
                userId: "george"
                //new List<Dtos.Common.Details>()
            );
        }
        private Magic.Domain.Models.Balance CreateBalance(BalanceDto balanceDto)
        {
            var newBalance= Magic.Domain.Models.Balance.Create(
                UserId: balanceDto.UserId,
                Amount: balanceDto.Amount,
                Description: balanceDto.Description,
                InvoiceId: balanceDto.InvoiceId,
                CreatedOn: balanceDto.CreatedOn,
                Before: balanceDto.Before,
                After: balanceDto.After,
                BillingAccount: balanceDto.BillingAccount,
                CurrentBalance: balanceDto.CurrentBalance,
                providerId: balanceDto.ProviderId
            );
            return newBalance;
        }
    }
}
*/