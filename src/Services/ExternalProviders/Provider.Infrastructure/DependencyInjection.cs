using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Provider.Application.Services.Masary;
using Provider.Application.Services.Momkn;
using Provider.Infrastructure.Services.External.Masary.Services;
using Provider.Infrastructure.Services.External.Momkn.Services;

namespace Provider.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IMasaryApiClient, MasaryApiClient>();
        services.AddHttpClient<IMomknApiClient, MomknApiClient>();
        return services;
    }
}
