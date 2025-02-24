using HealthChecks.UI.Client;
using Magic.Application.Common.Configurations;
using Magic.Application.Common.Interfaces;
using MagicServices.API.Middlewares;
using MagicServices.API.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

namespace MagicServices.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddHttpContextAccessor(); // Required for IHttpContextAccessor
        services.AddScoped<IUser, CurrentUser>(); // Register CurrentUser as IUser
        services.AddCarter();
        services.Configure<AppSettings>(configuration);
        //services.AddExceptionHandler<CustomExceptionHandler>();
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
            c.AddSecurityDefinition(name: "Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                }
            });
        });
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        //app.UseExceptionHandler(options => { });
        app.UseMiddleware<LanguageMiddleware>();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
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
            app.MapControllers(); // Map controllers
        }
        return app;
    }
}
