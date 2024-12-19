using BuildingBlocks.Exceptions.Handler;
using Carter;
using HealthChecks.UI.Client;
using MagicServices.API.Endpoints;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

namespace MagicServices.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCarter();

        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("Database")!);
        services.AddControllers();
        // Add Swagger generation
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Magic Services API",
                Version = "v1",
                Description = "API for Magic Services",
                Contact = new OpenApiContact
                {
                    Name = "Magic Services Team",
                    Email = "support@magicservices.com"
                }
            });
        });
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();

        app.UseExceptionHandler(options => { });
        app.UseHealthChecks("/health",
            new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Magic Services API v1");
            });
            app.UseRouting();
            app.MapControllers(); // Map controllers
        }
        return app;
    }
    public static WebApplication ConfigureEndpoints(this WebApplication app)
    {
        var servicesEndpoint = new ServicesEndpoint();
        servicesEndpoint.AddRoutes(app);
        var inquiryEndpoint = new InquiryEndpoint();
        inquiryEndpoint.AddRoutes(app);
        return app;
    }
}
