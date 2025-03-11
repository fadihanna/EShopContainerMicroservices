using Provider.Application.Configuration;
using Provider.Grpc.Services;

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

        public static WebApplication UseGrpcServices(this WebApplication app, IServiceCollection services)
        {
            app.MapGrpcService<ProviderInquiryService>();

            app.MapGrpcReflectionService();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            return app;
        }
    }
}
