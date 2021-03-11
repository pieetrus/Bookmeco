using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Infrastructure.Persistence
{
    public class Seed
    {
        public static async Task SeedData(IDataContext context)
        {
            var serviceCategory = new ServiceCategory { Name = "Test category" };
            context.ServiceCategories.Add(serviceCategory);
            var serviceCategories = new List<ServiceCategory>();
            serviceCategories.Add(serviceCategory);
            context.ServiceProviders.Add(new ServiceProvider { Name = "Test service provider", ServiceCategory = serviceCategories });

            await context.SaveChangesAsync();
        }
    }
}
