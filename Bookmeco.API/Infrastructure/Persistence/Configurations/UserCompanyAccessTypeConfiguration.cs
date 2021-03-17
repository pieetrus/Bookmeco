using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserCompanyAccessTypeConfiguration : IEntityTypeConfiguration<UserCompanyAccessType>
    {
        public void Configure(EntityTypeBuilder<UserCompanyAccessType> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(e => e.AccessLevel)
                .IsRequired();
        }
    }
}
