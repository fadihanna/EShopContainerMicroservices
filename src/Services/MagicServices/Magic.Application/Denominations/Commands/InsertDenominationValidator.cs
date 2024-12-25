namespace Magic.Application.Denominations.Commands
{
    public class InsertDenominationValidator : AbstractValidator<InsertDenominationCommand>
    {
        public InsertDenominationValidator()
        {
            RuleFor(x => x.Denomination.NameEN)
                .NotNull().WithMessage("NameEN cannot be null.")
                .NotEmpty().WithMessage("NameEN cannot be empty.");

            RuleFor(x => x.Denomination.NameAR)
                .NotNull().WithMessage("NameEN cannot be null.")
                .NotEmpty().WithMessage("NameEN cannot be empty.");

            RuleFor(x => x.Denomination.Value)
                .NotNull().WithMessage("Value cannot be null.")
                .NotEmpty().WithMessage("Value cannot be empty.");

            RuleFor(x => x.Denomination.MinValue)
                .NotNull().WithMessage("MinValue cannot be null.")
                .NotEmpty().WithMessage("MinValue cannot be empty.");

            RuleFor(x => x.Denomination.MaxValue)
                .NotNull().WithMessage("MaxValue cannot be null.")
                .GreaterThanOrEqualTo(x => x.Denomination.MinValue).WithMessage("Value must be greater than MinValue.")
                .NotEmpty().WithMessage("Id cannot be empty.");

            RuleFor(x => x.Denomination.ServiceId)
                .NotNull().WithMessage("ServiceId cannot be null.")
                .NotEqual(0).WithMessage("ServiceId must have value.")
                .NotEmpty().WithMessage("Id cannot be empty.");

            RuleFor(x => x.Denomination.PriceType)
                .NotNull().WithMessage("PriceType cannot be null.")
                .NotEqual(0).WithMessage("PriceType must have value.")
                .NotEmpty().WithMessage("PriceType cannot be empty.");
        }
    }
}
