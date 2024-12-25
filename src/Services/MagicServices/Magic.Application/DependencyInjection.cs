using BuildingBlocks.Behaviors;
using FluentValidation.AspNetCore;
using Magic.Application.Common.Inquiry.Commands;
using Magic.Application.Denominations.Commands;
using Magic.Application.Denominations.Queries.Denominations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using Serilog;
using System.Reflection;

namespace Magic.Application;
/// <summary>
/// README 
/// ###SECTION Magic.Application.DependencyInjection
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        // Register MediatR and Pipeline Behaviors
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        // Register Validation and Logging Pipeline Behaviors
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        // Register FluentValidation
        services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<InquiryCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<GetDenominationByIdValidator>();
        services.AddValidatorsFromAssemblyContaining<InsertDenominationValidator>();

        // Add Feature Management (optional, if used)
        services.AddFeatureManagement();

        // Add Serilog Logging
        services.AddSingleton(Log.Logger);

        return services;
    }
}
