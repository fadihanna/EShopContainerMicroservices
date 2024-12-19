using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magic.Infrastructure.Data.Configurations
{
    public class DenominationFeeConfiguration : IEntityTypeConfiguration<DenominationFee>
    {
        public void Configure(EntityTypeBuilder<DenominationFee> builder)
        {
            builder.ToTable(nameof(DenominationFee)).HasKey(e => e.Id);
            builder.HasOne(d => d.Denomination)
                .WithMany(d => d.DenominationFees)
                .HasForeignKey(d => d.DenominationId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
