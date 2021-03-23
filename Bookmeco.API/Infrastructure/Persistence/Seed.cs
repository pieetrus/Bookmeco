using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class Seed
    {
        public static async Task SeedData(IDataContext context, UserManager<User> userManager)
        {
            var somethingAdded = false;

            if (!userManager.Users.Any())
            {

                var adminRole = new Role { Name = "Admin", NormalizedName = "ADMIN" };
                var userRole = new Role { Name = "User", NormalizedName = "USER" };

                var users = new List<User>
                {
                    new User
                    {
                        UserName = "admin",
                        Email = "admin@admin.com",
                        Roles = new List<Role>{ adminRole }
                    },
                    new User
                    {
                        UserName = "user",
                        Email = "user@user.com",
                        Roles = new List<Role>{ userRole }
                    }
                };

                context.Roles.AddRange(adminRole, userRole);

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "123");
                }


                somethingAdded = true;
            }

            if (!context.CompanyCategories.Any())
            {
                var categoriesList = new List<CompanyCategory> { new CompanyCategory { Name = "Barber" }, new CompanyCategory { Name = "Manicure" } };
                context.CompanyCategories.AddRange(categoriesList);
                somethingAdded = true;
            }

            if (!context.Companies.Any())
            {
                var companiesList = new List<Company> { new Company { Name = "Test company", Address = "Test address", Latitude = 12, Longitude = 14},
                    new Company { Name = "Test company2", Address = "Test address2" } };
                context.Companies.AddRange(companiesList);
                somethingAdded = true;
            }

            if (somethingAdded)
                await context.SaveChangesAsync();
        }
    }
}
