using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Provider.Application.Data;
using Provider.Application.Logging;
using Provider.Application.Services.Masary;
using Provider.Domain.Repositories.Masary;
using Provider.Infrastructure.Data;
using Provider.Infrastructure.Mockup;
using Provider.Infrastructure.Repository.Masary;
using Provider.Infrastructure.Services.External.Masary.Services;

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

        services.AddHttpClient<IMasaryApiClient, MasaryApiClient>();
        services.AddTransient<ILogger<MasaryApiClient>, Logger<MasaryApiClient>>();
        services.AddTransient<MockHttpMessageHandler>();

        return services;
    }
}
