using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IDataContext
    {
         DbSet<ServiceProvider> ServiceProviders { get; set; }
         DbSet<ServiceCategory> ServiceCategories { get; set; }

         int SaveChanges();
         Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
