[MagicPro v1.1.1](http://useiconic.com/open)
===========

###SECTION Magic.Application.DependencyInjection
==================================================================

### SECTION 1
    **AddFluentValidationAutoValidation()
    Enables automatic server-side validation of models using FluentValidation.
    Automatically validates incoming DTOs or commands against the registered validators.

    **AddFluentValidationClientsideAdapters()
    Adds support for client - side validation in combination with ASP.NET Core's built-in validation features.
    Useful when working with Razor Pages or Blazor for frontend validation.

### SECTION 2
    **AddValidatorsFromAssemblyContaining<T>()
    Scans the assembly where the specified type (e.g., InquiryCommandValidator) exists 
    and registers all validators within that assembly.
        
