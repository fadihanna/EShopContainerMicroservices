using Microsoft.EntityFrameworkCore;
using Provider.Application.Data;
using Provider.Domain.Models;

namespace Provider.Infrastructure.Data
{
    public class ProviderDbContext : DbContext, IProviderDbContext
    {
        public ProviderDbContext(DbContextOptions<ProviderDbContext> options) : base(options) { }

        public DbSet<MasaryService> MasaryService { get; set; }
        public DbSet<MasaryServiceCharge> MasaryServiceCharge { get; set; }
        public DbSet<MasaryServiceParameter> MasaryServiceParameter { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProviderDbContext).Assembly);
        }
    }
}
