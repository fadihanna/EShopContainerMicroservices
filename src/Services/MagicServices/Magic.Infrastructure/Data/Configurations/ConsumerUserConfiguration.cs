using Magic.Infrastructure.Data.Identity.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magic.Infrastructure.Data.Configurations
{
    public class ConsumerUserConfiguration : IEntityTypeConfiguration<ConsumerUser>
    {
        public void Configure(EntityTypeBuilder<ConsumerUser> builder)
        {
            //builder.HasIndex(u => u.PhoneNumber)
            //        .IsUnique(true);  // Add uniqueness constraint on PhoneNumber

            //builder.HasIndex(u => u.Email)
            //        .IsUnique(true);  // Add uniqueness constraint on Email

            //// Remove the default indexes (username is not unique and normalized email also applied above"
            //builder.Metadata.RemoveIndex(builder.HasIndex(u => u.NormalizedUserName).Metadata);
            //builder.Metadata.RemoveIndex(builder.HasIndex(u => u.NormalizedEmail).Metadata);

        }
    }
}
