using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magic.Infrastructure.Data.Configurations
{
    public class DenominationInputParameterConfiguration : IEntityTypeConfiguration<DenominationInputParameter>
    {
        public void Configure(EntityTypeBuilder<DenominationInputParameter> builder)
        {
            builder.ToTable(nameof(DenominationInputParameter)).HasKey(e => e.Id);
            builder.HasOne(d => d.Denomination)
            .WithMany(d => d.DenominationInputParameters)
            .HasForeignKey(d => d.DenominationId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
