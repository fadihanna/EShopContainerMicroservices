using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Magic.Infrastructure.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // Find API Project Path (Assuming it's at the same level as Infrastructure)
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../MagicServices.Api");

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(basePath) // 👈 Set the base path to the API project
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true)
            .Build();

        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = configuration.GetConnectionString("Database");
        builder.UseSqlServer(connectionString);

        return new ApplicationDbContext(builder.Options);
    }
}
