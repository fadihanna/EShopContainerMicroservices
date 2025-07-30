using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Provider.Application.Data;
using Provider.Application.Repositories.Damen;
using Provider.Application.Services.Damen;
using Provider.Application.Services.Masary;
using Provider.Domain.Repositories.Masary;
using Provider.Infrastructure.Data;
using Provider.Infrastructure.Mockup;
using Provider.Infrastructure.Repository;
using Provider.Infrastructure.Repository.Masary;
using Provider.Infrastructure.Services.External.Damen.Services;
using Provider.Infrastructure.Services.External.Masary.Services;

namespace Provider.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
         services.AddDbContext<ProviderDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ProviderDb")));
        services.AddScoped<IProviderDbContext, ProviderDbContext>();
        services.AddScoped<IMasaryRepository, MasaryRepository>();
        services.AddScoped<IDamenRepository, DamenRepository>();
        services.AddHttpClient<IMasaryApiClient, MasaryApiClient>();
        services.AddHttpClient<IDamenApiClient, DamenApiClient>();
        services.AddTransient<MockHttpMessageHandler>();

 
        return services;
    }
}
