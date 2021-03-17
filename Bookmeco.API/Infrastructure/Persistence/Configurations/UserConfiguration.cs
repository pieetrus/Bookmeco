using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(e => e.Password)
                .HasMaxLength(60)
                .IsRequired();
        }
    }
}
