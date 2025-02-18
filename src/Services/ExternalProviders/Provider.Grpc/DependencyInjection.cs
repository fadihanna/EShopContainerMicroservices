using Provider.Application.Configuration;

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
