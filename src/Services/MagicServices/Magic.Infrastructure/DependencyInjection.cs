using Magic.Application.Exceptions;
using Magic.Application.Extensions;
using Magic.Application.Interfaces.Specifications;
using Magic.Domain.Enums;
using Magic.Domain.Specifications;
using Magic.Infrastructure.Data.Cache;
using Magic.Infrastructure.Data.Identity.Custom;
using Magic.Infrastructure.Data.Identity.Entity;
using Magic.Infrastructure.Data.Specifications;
using Magic.Infrastructure.Mapper;
using Magic.Infrastructure.Services.External;
using Magic.Infrastructure.Services.External.PaymentGateway;
using Magic.Infrastructure.Services.Internal;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PaymentGateway.Grpc.Protos;
using Provider.Grpc.Protos;
using System.Text;

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
        services.AddScoped<IProviderSpecification, ProviderSpecification>();
        services.AddScoped<IExternalProviderInquiryService, ExternalProviderInquiryService>();
        services.AddScoped<IExternalProviderFeesService, ExternalProviderFeesService>();
        services.AddScoped<ILookUpSpecification, LookUpSpecification>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<ILocalizationService, LocalizationService>();
        services.AddScoped<ILanguageService, LanguageService>();
        services.AddScoped<IUnitOfWork, ApplicationDbContext>();
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IRefreshTokenSpecification, RefreshTokenSpecification>();
        services.AddScoped<IDenominationGroupSpecification, DenominationGroupSpecification>();
        services.AddScoped<INotificationSpecification, NotificationSpecification>();

        services.AddScoped<IUserSpecification, UserSpecification>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<ITokenValidatorService, TokenValidatorService>();
        services.AddScoped<ITransactionSpecification, TransactionSpecification>();
        services.AddScoped<IPaymentGatewayClientService, PaymentGatewayClientService>();
        services.AddScoped<IRequestSepecification, RequestSpecification>();
        services.AddScoped<IServiceCategorySpecification, ServiceCategorySpecification>();
        services.AddScoped<IServiceSpecification, ServiceSpecification>();

        services.AddGrpcClient<ProviderInquiryProtoService.ProviderInquiryProtoServiceClient>(options =>
        {
            options.Address = new Uri("http://localhost:6001");
        });
        services.AddGrpcClient<ProviderFeesProtoService.ProviderFeesProtoServiceClient>(options =>
        {
            options.Address = new Uri("http://localhost:6001");
        });
        services.AddGrpcClient<PaymentGatewayProtoService.PaymentGatewayProtoServiceClient>(options =>
        {
            options.Address = new Uri("http://localhost:6002");
        });

        //Mapster
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(MappingConfig).Assembly);
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        // Configure Identity
        services.AddIdentity<ConsumerUser, IdentityRole<int>>(options =>
            {
                options.User.RequireUniqueEmail = true; // Ensures EmailIndex is unique
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddErrorDescriber<CustomIdentityErrorDescriber>();

        var jwtSettings = configuration.GetSection("IdentityConfig:JwtSettings");
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["ValidIssuer"],
            ValidAudience = jwtSettings["ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SigningKey"]))
        };
        services.AddSingleton(tokenValidationParameters);
        // Configure Authentication with JWT
        services.AddAuthentication(options =>
        {
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = false;
            options.TokenValidationParameters = tokenValidationParameters;
            options.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        throw new ForbiddenAccessException(InternalErrorCode.TokenExpired);
                    else
                        throw new ForbiddenAccessException(InternalErrorCode.Status401Unauthorized);
                },
                OnTokenValidated = context =>
                {
                    var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<ITokenValidatorService>();
                    return tokenValidatorService.ValidateAsync(context);
                },
                OnMessageReceived = context =>
                {
                    return Task.CompletedTask;
                },
                OnChallenge = context =>
                {
                    throw new ForbiddenAccessException(InternalErrorCode.Status401Unauthorized);
                },
                OnForbidden = context =>    
                {
                    throw new ForbiddenAccessException(InternalErrorCode.Status403Forbidden);
                },
            };
        });
        services.AddAuthorization();
        return services;
    }
}