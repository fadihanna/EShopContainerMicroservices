using Microsoft.EntityFrameworkCore;
using Provider.Application.Data;
using Provider.Domain.Models;
using System.Reflection;

namespace Provider.Infrastructure.Data
{
    public class ProviderDbContext : DbContext, IProviderDbContext
    {
        public ProviderDbContext(DbContextOptions<ProviderDbContext> options) : base(options) { }

        public DbSet<MasaryService> MasaryService => Set<MasaryService>();
        public DbSet<MasaryServiceCharge> MasaryServiceCharge => Set<MasaryServiceCharge>();
        public DbSet<MasaryServiceParameter> MasaryServiceParameter => Set<MasaryServiceParameter>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ProviderDbContext).Assembly);
        }
    }
}
