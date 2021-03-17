using Application.Common.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class Seed
    {
        public static async Task SeedData(IDataContext context)
        {
            var somethingAdded = false;

            if (!context.Categories.Any())
            {
                var categoriesList = new List<CompanyCategory> { new CompanyCategory { Name = "Barber" }, new CompanyCategory { Name = "Manicure" } };
                context.Categories.AddRange(categoriesList);
                somethingAdded = true;
            }

            if (!context.Companies.Any())
            {
                var companiesList = new List<Company> { new Company { Name = "Test company", Address = "Test address", Latitude = 12, Longitude = 14},
                    new Company { Name = "Test company2" } };
                context.Companies.AddRange(companiesList);
                somethingAdded = true;
            }

            if (somethingAdded)
                await context.SaveChangesAsync();
        }
    }
}
