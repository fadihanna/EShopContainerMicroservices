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
        public DbSet<DamenService> DamenServices { get; set; }
        public DbSet<DamenServiceField> DamenServiceFields { get; set; }
        public DbSet<DamenServiceCharge> DamenServiceCharges { get; set; }
        public DbSet<DamenServiceRequest> DamenServiceRequests { get; set; }
        public DbSet<DamenServiceChargeSlide> DamenServiceChargeSlide { get; set; }
        public DbSet<DamenServiceRequestInputField> DamenServiceRequestInputField { get; set; } 
        public DbSet<DamenServiceRequestOutputField> DamenServiceRequestOutputField { get; set; }
        public DbSet<FawryBiller> FawryBillers { get; set; }
        public DbSet<FawryBillerInfo> FawryBillerInfos { get; set; }
        public DbSet<FawryServiceCharge> ServiceCharge { get; set; }
        public DbSet<FawryPaymentItem> FawryPaymentItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DamenService>()
      .HasOne(s => s.Amount)
      .WithOne(a => a.DamenService)
      .HasForeignKey<DamenAmount>(a => a.DamenServiceId)
      .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DamenService>()
                .HasOne(s => s.ServiceCharge)
                .WithOne(sc => sc.DamenService)
                .HasForeignKey<DamenServiceCharge>(sc => sc.DamenServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DamenServiceCharge>()
                .HasMany(sc => sc.Slides)
                .WithOne(s => s.DamenServiceCharge)
                .HasForeignKey(s => s.DamenServiceChargeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DamenServiceRequestInputField>()
         .HasKey(x => new { x.DamenServiceRequestId, x.DataFieldId });

            modelBuilder.Entity<DamenServiceRequestInputField>()
                .HasOne(x => x.DamenServiceRequest)
                .WithMany(r => r.Inputs)
                .HasForeignKey(x => x.DamenServiceRequestId)
                .OnDelete(DeleteBehavior.Restrict); // ✅ الحل هنا

            modelBuilder.Entity<DamenServiceRequestInputField>()
                .HasOne(x => x.DataField)
                .WithMany()
                .HasForeignKey(x => x.DataFieldId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<DamenServiceRequestOutputField>()
                .HasKey(x => new { x.DamenServiceRequestId, x.DataFieldId });

            modelBuilder.Entity<DamenServiceRequestOutputField>()
                .HasOne(x => x.DamenServiceRequest)
                .WithMany(r => r.Outputs)
                .HasForeignKey(x => x.DamenServiceRequestId)
                .OnDelete(DeleteBehavior.Restrict); // ✅ برضو لازم Restrict

            modelBuilder.Entity<DamenServiceRequestOutputField>()
                .HasOne(x => x.DataField)
                .WithMany()
                .HasForeignKey(x => x.DataFieldId)
                .OnDelete(DeleteBehavior.Cascade);


        }



    }
}
