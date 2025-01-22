using Provider.Application;
using Provider.Application.Common;
using Provider.Grpc.Services;
using Provider.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc(options => { options.EnableDetailedErrors = true; });

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ProviderInquiryService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
