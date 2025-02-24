using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Provider.Application.Common;
using Provider.Application.Logging;
using Provider.Application.Services.Masary;
using System.Resources;

namespace Provider.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ResourceManager>(sp =>
                new ResourceManager("YourNamespace.Resources", typeof(DependencyInjection).Assembly));

            services.AddScoped<ProviderServiceInquiryImplement>();
            services.AddScoped<ExternalApiProviderFactory>();
            services.AddScoped<MasaryApiWrapper>();
            services.AddTransient<LoggingHandler>();
            services.AddTransient<ApiExceptionHandler>();
            services.AddHttpContextAccessor();
            services.AddTransient<ApiExceptionHandler>();

            return services;
        }
    }
}
