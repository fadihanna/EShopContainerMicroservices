using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Provider.Application.Common;
using Provider.Application.Services.Masary;
using Provider.Application.Services.Momkn;

namespace Provider.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ExternalApiProviderFactory>();
        services.AddScoped<ProviderServiceInquiryImplement>();
        services.AddScoped<MasaryApiWrapper>();
        services.AddScoped<MomknApiWrapper>();
        return services;
    }
}
