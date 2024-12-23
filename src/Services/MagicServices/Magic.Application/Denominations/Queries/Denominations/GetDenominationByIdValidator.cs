namespace Magic.Application.Denominations.Queries.Denominations
{
    public class GetDenominationByIdValidator : AbstractValidator<GetDenominationByIdQuery>
    {
        public GetDenominationByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id cannot be null.")
                .NotEqual(0).WithMessage("Id cannot be Zero.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.")
                .NotEmpty().WithMessage("Id cannot be empty.");
        }
    }
}
