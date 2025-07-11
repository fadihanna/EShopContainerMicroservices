﻿namespace Magic.Application.Common.Inquiry.Commands
{
    public class InquiryCommandValidator : AbstractValidator<InquiryCommand>
    {
        public InquiryCommandValidator()
        {
            RuleFor(x => x.Request)
                .NotNull().WithMessage("Inquiry request cannot be null.");

            RuleFor(x => x.Request.DenominationId)
                .NotNull().WithMessage("Denomination ID cannot be null.")
                .NotEqual(0).WithMessage("Denomination ID cannot be Zero.")
                .GreaterThan(0).WithMessage("Denomination ID must be greater than 0.")
                .NotEmpty().WithMessage("Denomination ID cannot be empty.");
        }
    }
}
