using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Provider.Application.Data;
using Provider.Application.Services.Masary;
using Provider.Domain.Repositories.Masary;
using Provider.Infrastructure.Data;
using Provider.Infrastructure.Mockup;
using Provider.Infrastructure.Repository.Masary;
using Provider.Infrastructure.Services.External.Masary.Services;

namespace Provider.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProviderDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(configuration.GetConnectionString("ProviderDb"));
        });
        services.AddHttpClient();
        services.AddScoped<IProviderDbContext, ProviderDbContext>();
        services.AddScoped<IMasaryRepository, MasaryRepository>();
        services.AddHttpClient<IMasaryApiClient, MasaryApiClient>();
        services.AddTransient<ILogger<MasaryApiClient>, Logger<MasaryApiClient>>();
        services.AddTransient<MockHttpMessageHandler>();

        return services;
    }
}
