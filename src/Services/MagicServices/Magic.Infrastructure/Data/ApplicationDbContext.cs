using Magic.Infrastructure.Data.Identity.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace Magic.Infrastructure.Data;
public class ApplicationDbContext : IdentityDbContext<ConsumerUser, IdentityRole<int>, int>, IApplicationDbContext, IUnitOfWork
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
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DbSet<ConsumerUser> ConsumerUsers => Set<ConsumerUser>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);

        builder.Entity<ConsumerUser>().ToTable("ConsumerUser");
        builder.Entity<IdentityRole<int>>().ToTable("ConsumerRole");
        builder.Entity<IdentityUserRole<int>>().ToTable("ConsumerUserRole");
        builder.Entity<IdentityUserClaim<int>>().ToTable("ConsumerUserClaim");
        builder.Entity<IdentityRoleClaim<int>>().ToTable("ConsumerRoleClaim");
        builder.Entity<IdentityUserLogin<int>>().ToTable("ConsumerUserLogin");
        builder.Entity<IdentityUserToken<int>>().ToTable("ConsumerUserToken");

        //builder.ApplyConfiguration(new ConsumerUserConfiguration());
    }
}
