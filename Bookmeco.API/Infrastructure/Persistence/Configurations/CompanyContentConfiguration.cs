using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CompanyContentConfiguration : IEntityTypeConfiguration<CompanyContent>
    {
        public void Configure(EntityTypeBuilder<CompanyContent> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(e => e.CompanyId)
                .IsRequired();
        }
    }
}
