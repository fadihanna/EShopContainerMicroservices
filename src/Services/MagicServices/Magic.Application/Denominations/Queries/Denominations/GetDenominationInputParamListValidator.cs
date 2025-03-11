namespace Magic.Application.Denominations.Queries.Denominations
{
    public class GetDenominationInputParamListValidator : AbstractValidator<GetDenominationByIdResponse>
    {
        public GetDenominationInputParamListValidator()
        {
            /*RuleForEach(x => x.denominationDto.InputParamterList)
                 .ChildRules(input =>
                 {
                     input.RuleFor(x => x.Value)
                     .NotEmpty()
                      .WithMessage(x => $"{x.Label} is required.")
                         .When(x => x.IsRequired);*/

                     //input.RuleFor(x => x.Value)
                     //.Must((x, value) => x == null || value == x.MaxLength)
                     //.WithMessage(x => $"{x.Label} must be exactly {x.MaxLength} characters long.")
                     //    .When(x => !string.IsNullOrEmpty(x.Value));
                // });
        }
    }
}
