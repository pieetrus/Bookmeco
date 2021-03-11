using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<ServiceProvider> ServiceProviders { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
    }
}
