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
        
        
### SECTION 3
    **That section is for Feature Flags / Feature Toggles.
    "FeatureManagement": {
      "OrderFullfilment": false
    }
    It means:
    The feature named OrderFullfilment is currently disabled.
    This is not built-in .NET behavior by itself — it works only if your project uses Microsoft.FeatureManagement.

    What Feature Management is
    Feature Management lets you turn features on/off without redeploying code, using configuration (appsettings, Azure App Configuration, etc.).

    Typical uses:
    1-Gradual rollout
    2-Kill switches
    3-Environment-specific features
    4-Beta / experimental features
    5-How it’s usually wired in code
    1- NuGet package
    dotnet add package Microsoft.FeatureManagement

    2- Register it
    builder.Services.AddFeatureManagement();

    3- Use it in code
    Option A: Attribute (very common)
    [FeatureGate("OrderFullfilment")]
    public class OrderController : ControllerBase
    {
        ...
    }

    ➡ Controller or endpoint is disabled when flag is false.

    Option B: Runtime check
    public class OrderService
    {
        public async Task ProcessAsync()
        {
            if (!await _featureManager.IsEnabledAsync("OrderFullfilment"))
                return;
            // feature logic
        }
    }

    What happens when it’s false
    Where used	Result
    [FeatureGate] on controller	HTTP 404
    [FeatureGate] on endpoint	Endpoint unavailable
    IsEnabledAsync	returns false

    Why teams use this
    In systems like yours (FinTech / Provider / Order flows), this is often used to:
    Disable payment flow instantly
    Disable settlement or fulfillment logic
    Roll out new providers gradually

    Example:
    "FeatureManagement": {
      "OrderFullfilment": false,
      "MasaryIntegration": true
    }

    Important notes
    The key name must exactly match the string used in code
    Typos (Fullfilment vs Fulfillment) matter
    By default it reads from appsettings.json, but can be overridden by:

    appsettings.Development.json
    environment variables
    Azure App Configuration

    Search for:
    AddFeatureManagement
    IFeatureManager
    [FeatureGate]
        
