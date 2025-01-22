using Magic.Application.Data;
using Magic.Domain.Specifications;
using Magic.Infrastructure.Data.Cache;
using Magic.Infrastructure.Data.Specifications;
using Magic.Infrastructure.Services.External;
using Magic.Infrastructure.Services.Internal;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Provider.Grpc.Protos;

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
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<IDenominationSpecification, DenominationSpecification>();
        services.AddScoped<IExternalProviderInquiryService, ExternalProviderInquiryService>();
        services.AddScoped<ILookUpSpecification, LookUpSpecification>();
        services.AddScoped<IInternalErrorCodeMapper, InternalErrorCodeMapper>(); 
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<ILocalizationService, LocalizationService>();
        services.AddScoped<ILanguageService, LanguageService>();
        services.AddGrpcClient<ProviderInquiryProtoService.ProviderInquiryProtoServiceClient>(options =>
        {
            options.Address = new Uri("http://localhost:6001");
        });
        return services;
    }
}