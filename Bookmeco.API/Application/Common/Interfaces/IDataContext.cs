using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IDataContext
    {
         DbSet<ServiceProvider> ServiceProviders { get; set; }
         DbSet<ServiceCategory> ServiceCategories { get; set; }
    }
}
