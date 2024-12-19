using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magic.Infrastructure.Data.Configurations
{
    public class DenominationProviderCodeConfiguration : IEntityTypeConfiguration<DenominationProviderCode>
    {
        public void Configure(EntityTypeBuilder<DenominationProviderCode> builder)
        {
            builder.ToTable(nameof(DenominationProviderCode)).HasKey(e => e.Id);
            builder.HasOne(d => d.Denomination)
            .WithMany(d => d.DenominationProviderCodes)
            .HasForeignKey(d => d.DenominationId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Provider)
                .WithMany(p => p.DenominationProviderCodes)
                .HasForeignKey(d => d.ProviderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
