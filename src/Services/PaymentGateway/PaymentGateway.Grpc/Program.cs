using MagicPaymentAPI.DTO;
using PaymentGateway.Grpc.ClientApi.Paymob;
using PaymentGateway.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddHttpClient("paymob");
builder.Services.AddScoped<IPayMobService, PayMobService>();

var app = builder.Build();

app.MapGrpcService<PaymentGatewayService>();
app.MapGrpcReflectionService();

app.MapGet("/", () => "Use a gRPC client to communicate with this service.");

foreach (var service in builder.Services)
{
    if (service.ServiceType.FullName.Contains("PaymentGateway.Grpc.ClientApi"))
        Console.WriteLine($"{service.ServiceType} -> {service.ImplementationType}");
}
app.Run();
