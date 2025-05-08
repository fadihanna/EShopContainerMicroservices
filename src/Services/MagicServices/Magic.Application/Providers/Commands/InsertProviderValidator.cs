namespace Magic.Application.Providers.Commands
{
    public class InsertProviderValidator : AbstractValidator<InsertProviderCommand>
    {
        public InsertProviderValidator()
        {
            RuleFor(x => x.NameEN)
                .NotNull().WithMessage("NameEN cannot be null.")
                .NotEmpty().WithMessage("NameEN cannot be empty.");

            RuleFor(x => x.NameAR)
                .NotNull().WithMessage("NameAR cannot be null.")
                .NotEmpty().WithMessage("NameAR cannot be empty.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive cannot be null.");

            RuleFor(x => x.SortOrder)
                .GreaterThanOrEqualTo(0).WithMessage("SortOrder must be 0 or greater.");
        }
    }
}
