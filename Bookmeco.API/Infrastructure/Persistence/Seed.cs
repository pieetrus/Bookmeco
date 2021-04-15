using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
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
                var serviceWorkerRole = new Role { Name = "ServiceWorker", NormalizedName = "SERVICEWORKER" };
                var clientRole = new Role { Name = "Client", NormalizedName = "CLIENT" };

                var users = new List<User>
                {
                    new User
                    {
                        UserName = "admin",
                        Email = "admin@admin.com",
                        Roles = new List<Role>{ adminRole },
                    },
                    new User
                    {
                        UserName = "user",
                        Email = "user@user.com",
                        Roles = new List<Role>{ userRole }
                    },
                    new User
                    {
                        UserName = "serviceWorker",
                        Email = "serviceWorker@worker.com",
                        Roles = new List<Role>{ userRole, serviceWorkerRole }
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


            if (!context.ServiceCategories.Any())
            {
                var serviceCategories = new List<ServiceCategory>
                {
                    new ServiceCategory {Name = "Hair only", Prize = 20, CreatedAt = DateTime.Now, ServiceDuration = 45},
                    new ServiceCategory {Name = "Beard", Prize = 10, CreatedAt = DateTime.Now, ServiceDuration = 20},
                    new ServiceCategory {Name = "Hair and beard", Prize = 30, CreatedAt = DateTime.Now, ServiceDuration = 60},
                };

                context.ServiceCategories.AddRange(serviceCategories);
                somethingAdded = true;
            }

            if (!context.Schedules.Any())
            {
                var entitiesList = new List<Schedule>
                {
                    new Schedule {CreatedAt = DateTime.Now, IsAvailable = true, UserId = 3},
                    new Schedule {CreatedAt = DateTime.Now, IsAvailable = true, UserId = 3},
                };

                context.Schedules.AddRange(entitiesList);
                somethingAdded = true;
            }

            if (!context.ScheduleDays.Any())
            {
                var entitiesList = new List<ScheduleDay>
                {
                    new ScheduleDay
                    {
                        ScheduleId = 1,
                        BeginTime = 28800,  //  8:00
                        EndTime = 57600, //  16:00
                        Date =  new DateTime(2021, 12,2),
                        DayOfWeek = DayOfWeek.Monday,
                        IsRegular = false,
                        MaxClients = 10
                    },
                    new ScheduleDay
                    {
                        ScheduleId = 1,
                        BeginTime = 28800,  //  8:00
                        EndTime = 57600, //  16:00
                        DayOfWeek = DayOfWeek.Monday,
                        IsRegular = false,
                        MaxClients = 10
                    },
                };

                context.ScheduleDays.AddRange(entitiesList);
                somethingAdded = true;
            }

            if (!context.Reservations.Any())
            {
                var entitiesList = new List<Reservation>
                {
                    new Reservation
                    {
                        UserId = 1,
                        Prize = 15,
                        ReservationDuration = 45,
                        ServiceCategoryId = 1,
                        ScheduleId = 1,
                        Date = DateTime.Now
                    },
                    new Reservation
                    {
                        UserId = 1,
                        Prize = 15,
                        ReservationDuration = 45,
                        ServiceCategoryId = 1,
                        ScheduleId = 1,
                        Date = DateTime.Now
                    }
                };

                context.Reservations.AddRange(entitiesList);
                somethingAdded = true;
            }


            if (somethingAdded)
                await context.SaveChangesAsync();
        }
    }
}
