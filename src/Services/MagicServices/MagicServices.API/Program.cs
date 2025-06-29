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

builder.Services.AddEndpointsApiExplorer();
// Configure Serilog from appsettings.json
builder.Host.UseSerilog((context, services, configuration) =>
{
    //configuration
    //    .ReadFrom.Configuration(context.Configuration)
    //    .Enrich.FromLogContext();
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
app.UseApiServices();
//app.ConfigureEndpoints();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Required to map attribute-based controllers
});

app.Run();
