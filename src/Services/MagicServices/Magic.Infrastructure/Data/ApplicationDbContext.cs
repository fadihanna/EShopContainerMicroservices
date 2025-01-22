using Magic.Application.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;

namespace Magic.Infrastructure.Data;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }


    public DbSet<Lookups.Provider> Providers => Set<Lookups.Provider>();
    public DbSet<InternalErrorCodeLookup> InternalErrorCodeLookups => Set<InternalErrorCodeLookup>();
    public DbSet<ServiceCategory> ServiceCategories => Set<ServiceCategory>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Denomination> Denominations => Set<Denomination>();
    public DbSet<DenominationFee> DenominationFees => Set<DenominationFee>();
    public DbSet<DenominationGroup> DenominationGroups => Set<DenominationGroup>();
    public DbSet<DenominationInputParameter> DenominationInputParameters => Set<DenominationInputParameter>();
    public DbSet<DenominationProviderCode> DenominationProviderCodes => Set<DenominationProviderCode>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.ConfigureWarnings(warnings =>
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning)); // Ignore PendingModelChangesWarning
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
