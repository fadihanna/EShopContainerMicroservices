using Magic.Application;
using Magic.Infrastructure;
using Magic.Infrastructure.Data.Extensions;
using MagicServices.API;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

//TODO: Configure Kestrel for Docker
//var port = Environment.GetEnvironmentVariable("ASPNETCORE_HTTP_PORTS");
//var ports = Environment.GetEnvironmentVariable("ASPNETCORE_HTTPS_PORTS");
//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.ListenAnyIP(int.Parse(port)); // Match Docker EXPOSE and ports mapping
//    serverOptions.ListenAnyIP(int.Parse(ports), listenOptions => listenOptions.UseHttps());
//});
builder.Services.AddEndpointsApiExplorer();
// Configure Serilog from appsettings.json
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .Enrich.FromLogContext()
        .WriteTo.Console();  // Only console, no SQL Server yet
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});



var app = builder.Build();
app.UseApiServices();
// Now reconfigure Serilog with SQL Server sink after database exists
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Required to map attribute-based controllers
});

app.Run();
