using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Provider.Infrastructure.Data
{
    public class ProviderDbContextFactory : IDesignTimeDbContextFactory<ProviderDbContext>
    {
        public ProviderDbContext CreateDbContext(string[] args)
        {
            // Find API Project Path (Assuming it's at the same level as Infrastructure)
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../Provider.Grpc");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath) // 👈 Set the base path to the API project
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true)
                .Build();

            var builder = new DbContextOptionsBuilder<ProviderDbContext>();
            var connectionString = configuration.GetConnectionString("ProviderDb");
            builder.UseSqlServer(connectionString);

            return new ProviderDbContext(builder.Options);
        }
    }
}
