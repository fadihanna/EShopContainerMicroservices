using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magic.Infrastructure.Data.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable(nameof(Service)).HasKey(e => e.Id);
            builder.HasOne(s => s.ServiceCategory)
                .WithMany()
                .HasForeignKey(s => s.ServiceCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
