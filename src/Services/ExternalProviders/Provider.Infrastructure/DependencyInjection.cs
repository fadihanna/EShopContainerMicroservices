using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Provider.Application.Data;
using Provider.Application.Services.Masary;
using Provider.Application.Services.Momkn;
using Provider.Infrastructure.Services.External.Masary.Services;
using Provider.Infrastructure.Services.External.Momkn.Services;
using Provider.Domain.Repositories.Masary;
using Provider.Infrastructure.Repository.Masary;
using Provider.Infrastructure.Data;
using Microsoft.Extensions.Hosting;
using Provider.Infrastructure.Mockup;
using Microsoft.Extensions.Logging;
using Provider.Application.Logging;

namespace Provider.Infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddDbContext<ProviderDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ProviderDb")));

        services.AddScoped<IProviderDbContext>(provider => provider.GetRequiredService<ProviderDbContext>());
        services.AddScoped<IMasaryRepository, MasaryRepository>();

        if (environment.IsDevelopment())
        {
            services.AddHttpClient<IMasaryApiClient, MasaryApiClient>()
                .ConfigurePrimaryHttpMessageHandler(() => new MockHttpMessageHandler());
        }
        else
        {
            services.AddHttpClient<IMasaryApiClient, MasaryApiClient>()
                .AddHttpMessageHandler<LoggingHandler>();
        }

        services.AddHttpClient<IMomknApiClient, MomknApiClient>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));
        services.AddHttpClient<IMasaryApiClient, MasaryApiClient>();
        services.AddTransient<ILogger<MasaryApiClient>, Logger<MasaryApiClient>>();
        services.AddTransient<MockHttpMessageHandler>();

        return services;
    }
}
