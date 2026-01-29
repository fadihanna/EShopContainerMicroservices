//TODO
//use decorator pattern to add new features to existing classes without modifying their structure.
//e.g., logging, caching, validation, etc.
//https://refactoring.guru/design-patterns/decorator
//
[]: #     This allows for easy addition of new validators without modifying existing registration code.
[]: # 
[]: # ### SECTION 3
[]: #
[]: # The combination of these methods in the AddApplicationServices extension method
[]: # provides a streamlined way to set up FluentValidation in an ASP.NET Core application.
[]: # It ensures that both server-side and client-side validation are properly configured,
[]: # and it automatically registers all validators from the specified assembly,
[]: # promoting a clean and maintainable codebase.
[]: # 
[]: # ### Example Usage
[]: # 
[]: # This sets up FluentValidation with automatic validation and client-side adapters,
[]: # while also registering all validators from the assembly containing InquiryCommandValidator.
[]: # 
[]: # ### Conclusion
[]: # 
[]: # The AddApplicationServices extension method simplifies the integration of FluentValidation
[]: # into an ASP.NET Core application, promoting best practices for validation and maintainability.
[]: #
[]: # ==================================================================
[]: # ### End of Magic.Application.DependencyInjection
# MagicPro v1.1.1
# ===========
#
# Magic.Application.DependencyInjection
# ==================================================================
#
# SECTION Magic.Application.DependencyInjection
# ==================================================================
#
# The AddApplicationServices extension method is designed to configure
# FluentValidation services within an ASP.NET Core application.
# It provides a convenient way to set up automatic validation
# for incoming requests and register validators from a specific assembly.
# This method is typically called during the service registration phase
# in the Startup.cs or Program.cs file of an ASP.NET Core application.
public static class ApplicationServiceExtensions
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		// SECTION 1
		services.AddFluentValidationAutoValidation();
		services.AddFluentValidationClientsideAdapters();
		// SECTION 2
		services.AddValidatorsFromAssemblyContaining<InquiryCommandValidator>();
		return services;
	}
}
#
# The combination of these methods in the AddApplicationServices extension method
# provides a streamlined way to set up FluentValidation in an ASP.NET Core application.
# It ensures that both server-side and client-side validation are properly configured,
# and it automatically registers all validators from the specified assembly,
# promoting a clean and maintainable codebase.
#
# Example Usage
#
# In your Startup.cs or Program.cs, you can use the AddApplicationServices method like this:
# ```csharp
# public void ConfigureServices(IServiceCollection services)
# {
#     services.AddApplicationServices();
#     // Other service registrations...
# }
# ```
#
# This sets up FluentValidation with automatic validation and client-side adapters,
# while also registering all validators from the assembly containing InquiryCommandValidator.
#
# Conclusion
#
# The AddApplicationServices extension method simplifies the integration of FluentValidation
# into an ASP.NET Core application, promoting best practices for validation and maintainability.
#
# ==================================================================
# End of Magic.Application.DependencyInjection
