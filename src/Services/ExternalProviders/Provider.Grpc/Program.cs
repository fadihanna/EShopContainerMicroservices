using Provider.Application.Logging;
using Provider.Application.Services.Masary;
using Provider.Grpc;
using Provider.Infrastructure;
using Provider.Infrastructure.Mockup;
using Provider.Infrastructure.Services.External.Masary.Services;
using Serilog;
using Provider.Application;
using Provider.Application.Configuration;
using Microsoft.EntityFrameworkCore;
using Provider.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddInfrastructureServices(builder.Configuration)
    .AddGrpcServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration);
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
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext();
});
 
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("Appsettings"));

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

builder.Services.AddHttpContextAccessor();
var app = builder.Build();
app.UseGrpcServices(builder.Services);

app.Run();