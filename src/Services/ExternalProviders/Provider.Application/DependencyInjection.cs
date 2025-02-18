using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Provider.Application.Common.Helpers;
using Provider.Application.Logging;
using Provider.Application.Services.Masary;
using Provider.Domain.Repositories.Masary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ResourceManager>(sp =>
                new ResourceManager("YourNamespace.Resources", typeof(DependencyInjection).Assembly));

             
            services.AddHttpContextAccessor();
            services.AddTransient<ApiExceptionHandler>();

            return services;
        }
    }
}
