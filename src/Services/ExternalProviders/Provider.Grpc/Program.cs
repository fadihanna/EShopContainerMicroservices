using Microsoft.EntityFrameworkCore;
using Provider.Application.Common;
using Provider.Application.Common.Helpers;
using Provider.Application.Configuration;
using Provider.Application.Data;
using Provider.Application.Logging;
using Provider.Application.Services.Masary;
using Provider.Domain.Repositories.Masary;
using Provider.Grpc.Services;
using Provider.Infrastructure.Data;
using Provider.Infrastructure.Mockup;
using Provider.Infrastructure.Repository.Masary;
using Provider.Infrastructure.Services.External.Masary.Services;
using Serilog;
using System.Resources;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext();
});

builder.Services.AddSingleton<ResourceManager>(sp =>
   new ResourceManager("YourNamespace.Resources", typeof(Program).Assembly));


builder.Services.AddGrpc();

builder.Services.AddGrpcReflection();
builder.Services.AddDbContext<ProviderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProviderDb")));

builder.Services.AddScoped<IProviderDbContext, ProviderDbContext>();
builder.Services.AddHttpClient<IMasaryApiClient, MasaryApiClient>();
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddHttpClient<IMasaryApiClient, MasaryApiClient>()
        .ConfigurePrimaryHttpMessageHandler(() => new MockHttpMessageHandler());
}
else
{
    builder.Services.AddHttpClient<IMasaryApiClient, MasaryApiClient>()
        .AddHttpMessageHandler<LoggingHandler>();
}

builder.Services.AddTransient<LoggingHandler>();

builder.Services.AddScoped<IMasaryRepository, MasaryRepository>();
builder.Services.AddScoped<ProviderServiceInquiryImplement>();
builder.Services.AddScoped<ExternalApiProviderFactory>();
builder.Services.AddScoped<MasaryApiWrapper>();
builder.Services.AddHttpClient();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("Appsettings"));

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ApiExceptionHandler>();
var app = builder.Build();

app.MapGrpcService<ProviderInquiryService>();
app.MapGrpcReflectionService();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.Run();