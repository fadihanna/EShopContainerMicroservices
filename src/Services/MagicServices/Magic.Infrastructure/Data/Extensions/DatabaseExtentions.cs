using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Magic.Infrastructure.Data.Extensions;
public static class DatabaseExtentions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.MigrateAsync().GetAwaiter().GetResult();
        await SeedAsync(context);
    }
    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedProviderAsync(context);
        await SeedServiceCategoryAsync(context);
        await SeedServiceAsync(context);
        await SeedDenominationAsync(context);
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
    private static async Task SeedDenominationAsync(ApplicationDbContext context)
    {
        if (!await context.Denominations.AnyAsync())
        {
            await context.Denominations.AddRangeAsync(InitialData.Denominations);
            await context.SaveChangesAsync();
        }
    }
}