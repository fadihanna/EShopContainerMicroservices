using Magic.Infrastructure.Data.Identity.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magic.Infrastructure.Data.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable(nameof(RefreshToken)).HasKey(e => e.Id);
            builder.HasOne(d => d.ConsumerUser)
                .WithMany()
                .HasForeignKey(d => d.ConsumerUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
