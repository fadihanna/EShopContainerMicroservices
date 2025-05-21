namespace Magic.Application.Common.Payment.Commands
{
    public class InsertTransactionCommandValidator : AbstractValidator<InsertTransactionCommand>
    {
        public InsertTransactionCommandValidator()
        {
            RuleFor(x => x.Transaction.PaymentProviderId)
                .NotNull().WithMessage("Payment Provider cannot be null.")
                .NotEqual(0).WithMessage("Payment Provider cannot be Zero");

            RuleFor(x => x.Transaction.Amount)
                 .NotNull().WithMessage("Amount cannot be null.")
                 .NotEqual(0).WithMessage("Amount cannot be Zero.")
                 .GreaterThan(0).WithMessage("Amount must be greater than 0.");

            RuleFor(x => x.Transaction.Fees)
                 .NotNull().WithMessage("Amount cannot be null.");

            RuleFor(x => x.Transaction.TotalAmount)
               .NotNull().WithMessage("Total Amount cannot be null.")
               .NotEqual(0).WithMessage("Total Amount cannot be Zero.")
               .GreaterThan(0).WithMessage("Total Amount must be greater than 0.");


            RuleFor(x => x.Transaction.DenominationId)
              .NotNull().WithMessage("DenominationId cannot be null.")
              .NotEqual(0).WithMessage("DenominationId cannot be Zero")
              .NotEmpty().WithMessage("DenominationId cannot be empty.");
          
        }
    }
}
