using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Provider.Infrastructure.Data.Extensions
{
    public static class DatabaseExtentions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            try
            {
                using var scope = app.Services.CreateScope();
                ILogger logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("DatabaseExtention");
                var context = scope.ServiceProvider.GetRequiredService<ProviderDbContext>();

                AsyncRetryPolicy retry = Policy
                    .Handle<SqlException>()
                    .Or<InvalidOperationException>()
                    .Or<TimeoutException>()
                    .WaitAndRetryAsync(
                        retryCount: 2,
                        sleepDurationProvider: attempt =>
                            TimeSpan.FromSeconds(Math.Pow(2, attempt)) +
                            TimeSpan.FromMilliseconds(Random.Shared.Next(0, 250)),
                        onRetry: (exception, timeSpan, retryCount, context) =>
                        {
                            logger.LogWarning(exception, "Exception {ExceptionType} with message {Message} detected on attempt {RetryAttempt} of {Retries} when trying to connect to database",
                                exception.GetType().Name, exception.Message, retryCount, 2);
                        }
                    );
                await retry.ExecuteAsync(async () =>
                {
                    var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
                    if (pendingMigrations.Any())
                        await context.Database.MigrateAsync();

                    if (await context.MasaryService.AnyAsync()) return;
                    await SeedAsync(context, app);
                });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private static async Task SeedAsync(ProviderDbContext context, WebApplication webApplication)
        {
            await SeedMasaryServiceAsync(context);
            await SeedMasaryServiceChargeAsync(context);
            await SeedMasaryServiceParameterAsync(context);
        }
        private static async Task SeedMasaryServiceAsync(ProviderDbContext context)
        {
            if (!await context.MasaryService.AnyAsync())
            {
                await context.MasaryService.AddRangeAsync(InitialData.MasaryServices);
                await context.SaveChangesAsync();
            }
        }
        private static async Task SeedMasaryServiceChargeAsync(ProviderDbContext context)
        {
            if (!await context.MasaryServiceCharge.AnyAsync())
            {
                await context.MasaryServiceCharge.AddRangeAsync(InitialData.MasaryServiceCharges);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedMasaryServiceParameterAsync(ProviderDbContext context)
        {
            if (!await context.MasaryServiceParameter.AnyAsync())
            {
                await context.MasaryServiceParameter.AddRangeAsync(InitialData.MasaryServiceParameters);
                await context.SaveChangesAsync();
            }
        }
    }
}
