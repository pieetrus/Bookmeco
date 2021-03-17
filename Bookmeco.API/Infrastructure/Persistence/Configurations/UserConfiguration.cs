using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(60);
        }
    }
}
