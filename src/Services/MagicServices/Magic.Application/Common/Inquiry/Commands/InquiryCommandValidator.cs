namespace Magic.Application.Common.Inquiry.Commands
{
    public class InquiryCommandValidator : AbstractValidator<InquiryCommand>
    {
        public InquiryCommandValidator()
        {
            RuleFor(x => x.Request)
                .NotNull().WithMessage("Inquiry request cannot be null.");

            RuleFor(x => x.Request.BillingAccount)
                .NotEmpty().WithMessage("Billing Account is required.");

            RuleFor(x => x.Request.ExternalId)
                .NotEmpty().WithMessage("External ID is required.")
                .MaximumLength(50).WithMessage("External ID cannot exceed 50 characters.");

            RuleFor(x => x.Request.DenominationId)
                .NotEmpty().WithMessage("Denomination ID is required.");
        }
    }
}
