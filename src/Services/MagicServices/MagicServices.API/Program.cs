using Magic.Application;
using Magic.Domain.Models;
using Magic.Infrastructure;
using Magic.Infrastructure.Data.Extensions;
using MagicServices.API;
using Microsoft.OpenApi.Models;
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
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext();
});
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "magic.API", Version = "v2" });
    c.CustomSchemaIds(type => type.ToString());
});
var app = builder.Build();
app.MapControllers();
// Configure the HTTP request pipeline.
app.UseApiServices();
//app.ConfigureEndpoints();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
app.Run();

