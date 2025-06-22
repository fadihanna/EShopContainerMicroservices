using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Magic.Infrastructure.Data.Extensions;
public static class DatabaseExtentions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

         var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
        await SeedAsync(context, app);

        if (pendingMigrations.Any())
        {
            await context.Database.MigrateAsync();
            return;

        }

         if (await context.Providers.AnyAsync()) return;

    }

    private static async Task SeedAsync(ApplicationDbContext context, WebApplication webApplication)
    {
        await SeedProviderAsync(context);
        await SeedServiceCategoryAsync(context);
        await SeedServiceAsync(context);
        await SeedDenominationGroupAsync(context);
        await SeedDenominationAsync(context);
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
}