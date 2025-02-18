using MassTransit;
using Microsoft.EntityFrameworkCore;
using Provider.Application.Data;
using Provider.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Infrastructure.Data
{
    public class ProviderDbContext : DbContext, IProviderDbContext
    {
        public ProviderDbContext(DbContextOptions<ProviderDbContext> options) : base(options) { }

        public DbSet<MasaryService> MasaryServiceslists { get; set; }
        public DbSet<MasaryServiceCharge> ServiceCharges { get; set; }
        public DbSet<MasaryServiceParameter> ServiceParameters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProviderDbContext).Assembly);


        }
    }
}
