using PaymentGateway.Grpc.ClientApi;
using PaymentGateway.Grpc.ClientApi.EbeGateway;
using PaymentGateway.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IEbeGatewayService, EbeGatewayService>();

builder.Services.AddScoped<IPaymentProvider, EbePaymentProvider>();

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

var app = builder.Build();

app.MapGrpcService<PaymentGatewayService>();
app.MapGrpcReflectionService();

app.MapGet("/", () => "Use a gRPC client to communicate with this service.");

app.Run();
