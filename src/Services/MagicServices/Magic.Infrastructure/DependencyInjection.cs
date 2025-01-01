using Magic.Application.Data;
using Magic.Domain.Specifications;
using Magic.Infrastructure.Data.Cache;
using Magic.Infrastructure.Data.Specifications;
using Magic.Infrastructure.Services.External;
using Magic.Infrastructure.Services.External.Masary.Services;
using Magic.Infrastructure.Services.External.Momkn.Services;
using Magic.Infrastructure.Services.Internal;
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
        services.AddMemoryCache();
        services.AddTransient<MomknApiClient>();
        services.AddTransient<MasaryApiClient>();
        services.AddTransient<MasaryApiWrapper>();
        services.AddTransient<MomknApiWrapper>();
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<IExternalApiProviderFactory, ExternalApiProviderFactory>();
        services.AddScoped<IDenominationSpecification, DenominationSpecification>();
        services.AddScoped<ILookUpSpecification, LookUpSpecification>();
        services.AddScoped<IInternalErrorCodeMapper, InternalErrorCodeMapper>(); 
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<ILocalizationService, LocalizationService>();
        services.AddScoped<ILanguageService, LanguageService>();
        return services;
    }
}