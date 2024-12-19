using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magic.Infrastructure.Data.Configurations
{
    public class DenominationConfiguration : IEntityTypeConfiguration<Denomination>
    {
        public void Configure(EntityTypeBuilder<Denomination> builder)
        {
            builder.ToTable(nameof(Denomination)).HasKey(e => e.Id);
            builder.HasOne(d => d.Provider)
                .WithMany()
                .HasForeignKey(d => d.ProviderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Service)
                .WithMany()
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
