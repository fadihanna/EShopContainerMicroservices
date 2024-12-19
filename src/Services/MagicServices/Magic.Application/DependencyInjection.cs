using BuildingBlocks.Behaviors;
using FluentValidation.AspNetCore;
using Magic.Application.Common.Inquiry.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
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
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<InquiryCommandValidator>();

        services.AddFeatureManagement();
        //services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

        return services;
    }
}
