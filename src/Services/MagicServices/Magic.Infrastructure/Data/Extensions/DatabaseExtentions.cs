using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Magic.Infrastructure.Data.Extensions;
public static class DatabaseExtentions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        ILogger logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("DatabaseExtention");
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        AsyncRetryPolicy retry = Policy
            .Handle<SqlException>()
            .Or<InvalidOperationException>()
            .Or<TimeoutException>()
            .WaitAndRetryAsync(
                retryCount: 6,
                sleepDurationProvider: attempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, attempt)) +
                    TimeSpan.FromMilliseconds(Random.Shared.Next(0, 250)),
                onRetry: (exception, timeSpan, retryCount, context) =>
                {
                    logger.LogWarning(exception, "Exception {ExceptionType} with message {Message} detected on attempt {RetryAttempt} of {Retries} when trying to connect to database",
                        exception.GetType().Name, exception.Message, retryCount, 6);
                }
            );
        await retry.ExecuteAsync(async () =>
        {
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                await context.Database.MigrateAsync();

            if (await context.Providers.AnyAsync()) return;
            await SeedAsync(context, app);
        });
    }

    private static async Task SeedAsync(ApplicationDbContext context, WebApplication webApplication)
    {
        await SeedProviderAsync(context);
        await SeedServiceCategoryAsync(context);
        await SeedServiceAsync(context);
        await SeedDenominationGroupAsync(context);
        await SeedDenominationAsync(context);
        await SeedDenominationProviderAsync(context);
        await SeedPaymentProviderAsync(context);
        await IdentitySeeder.SeedIdentityAsync(webApplication);
    }
    private static async Task SeedProviderAsync(ApplicationDbContext context)
    {
        if (!await context.Providers.AnyAsync())
        {
            await context.Providers.AddRangeAsync(InitialData.Providers);
            await context.SaveChangesAsync();
        }
    }
    private static async Task SeedServiceCategoryAsync(ApplicationDbContext context)
    {
        if (!await context.ServiceCategories.AnyAsync())
        {
            await context.ServiceCategories.AddRangeAsync(InitialData.ServiceCategories);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedServiceAsync(ApplicationDbContext context)
    {
        if (!await context.Services.AnyAsync())
        {
            await context.Services.AddRangeAsync(InitialData.Services);
            await context.SaveChangesAsync();
        }
    }
    private static async Task SeedDenominationGroupAsync(ApplicationDbContext context)
    {
        if (!await context.DenominationGroups.AnyAsync())
        {
            await context.DenominationGroups.AddRangeAsync(InitialData.DenominationGroups);
            await context.SaveChangesAsync();
        }
    }
    private static async Task SeedDenominationAsync(ApplicationDbContext context)
    {
        if (!await context.Denominations.AnyAsync())
        {
            await context.Denominations.AddRangeAsync(InitialData.Denominations);
            await context.SaveChangesAsync();
        }
    }
    private static async Task SeedDenominationProviderAsync(ApplicationDbContext context)
    {
        if (!await context.DenominationProviderCodes.AnyAsync())
        {
            await context.DenominationProviderCodes.AddRangeAsync(InitialData.DenominationProviderCodes);
            await context.SaveChangesAsync();
        }
    }
    private static async Task SeedPaymentProviderAsync(ApplicationDbContext context)
    {
        if (!await context.PaymentProviders.AnyAsync())
        {
            await context.PaymentProviders.AddRangeAsync(InitialData.PaymentProviders);
            await context.SaveChangesAsync();
        }
    }
}