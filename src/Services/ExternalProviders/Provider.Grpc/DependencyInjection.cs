using Provider.Application.Common;
using Provider.Application.Configuration;
using Provider.Application.Services.Masary;
using Provider.Application.Services.Momkn;

namespace Provider.Grpc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGrpcServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpc(options => { options.EnableDetailedErrors = true; });

            services.Configure<AppSettings>(configuration);

            return services;
        }
    }
}
