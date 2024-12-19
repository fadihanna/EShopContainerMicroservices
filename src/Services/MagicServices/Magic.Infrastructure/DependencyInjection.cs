using Magic.Application.Data;
using Magic.Infrastructure.Services.External;
using Magic.Infrastructure.Services.External.Masary.Services;
using Magic.Infrastructure.Services.External.Momkn.Services;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Magic.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        // Add services to the container.
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString);
        });
        services.AddHttpClient();
        services.AddTransient<MomknApiClient>();
        services.AddTransient<MasaryApiClient>();
        services.AddTransient<MasaryApiWrapper>();
        services.AddTransient<MomknApiWrapper>();
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<IExternalApiProviderFactory, ExternalApiProviderFactory>();
        return services;
    }
}