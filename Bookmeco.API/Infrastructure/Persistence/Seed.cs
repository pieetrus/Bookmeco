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

            if (!userManager.Users.Any())
            {

                var adminRole = new Role { Name = "Admin", NormalizedName = "ADMIN" };
                var userRole = new Role { Name = "Client", NormalizedName = "CLIENT" };
                var serviceProviderRole = new Role { Name = "Service Provider", NormalizedName = "SERVICE PROVIDER" };
                var serviceWorkerRole = new Role { Name = "Service Worker", NormalizedName = "SERVICE WORKER" };

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
                        Roles = new List<Role>{ userRole }
                    },
                    new User
                    {
                        UserName = "serviceOwner",
                        Email = "serviceOwner@worker.com",
                        Roles = new List<Role>{ userRole }
                    }
                };

                context.Roles.AddRange(adminRole, userRole);

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "123");
                }


                await context.SaveChangesAsync();
            }

            if (!context.Companies.Any())
            {
                var categoriesList = new List<CompanyCategory>
                {
                    new CompanyCategory { Name = "Barber", },
                    new CompanyCategory { Name = "Manicure" },
                };
                context.CompanyCategories.AddRange(categoriesList);

                var companiesList = new List<Company>
                {
                    new Company { Name = "Test company", Address = "Test address", Latitude = 12, Longitude = 14, Categories = new List<CompanyCategory>{ categoriesList[0], categoriesList[1]}},
                    new Company { Name = "Test company2", Address = "Test address2",  }
                };
                context.Companies.AddRange(companiesList);
                await context.SaveChangesAsync();
            }

            if (!context.CompanyContents.Any())
            {
                var entitiesList = new List<CompanyContent>
                {
                    new CompanyContent
                    {
                        CompanyId = 1,
                        Content = "Company content test",
                        CreatedAt = DateTime.Now,
                        Name = "Berserk"
                    }
                };

                context.CompanyContents.AddRange(entitiesList);
                await context.SaveChangesAsync();
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
                await context.SaveChangesAsync();

                var users = context.Users.ToList();
                users[0].ServiceCategories = new List<ServiceCategory> { serviceCategories[0] };
                users[1].ServiceCategories = new List<ServiceCategory> { serviceCategories[0], serviceCategories[1] };
                await context.SaveChangesAsync();
            }

            if (!context.Schedules.Any())
            {
                var entitiesList = new List<Schedule>
                {
                    new Schedule {CreatedAt = DateTime.Now, IsAvailable = true, UserId = 3},
                    new Schedule {CreatedAt = DateTime.Now, IsAvailable = true, UserId = 3},
                };

                context.Schedules.AddRange(entitiesList);
                await context.SaveChangesAsync();
            }

            if (!context.ScheduleDays.Any())
            {
                var entitiesList = new List<ScheduleDay>
                {
                    new ScheduleDay
                    {
                        ScheduleId = 1,
                        BeginTime = new DateTime(2021, 12,2, 8, 0, 0),
                        EndTime = new DateTime(2021, 12,2, 16, 0, 0),
                        DayOfWeek = DayOfWeek.Monday,
                        IsRegular = false,
                        MaxClients = 10
                    },
                    new ScheduleDay
                    {
                        ScheduleId = 1,
                        BeginTime = new DateTime(2021, 12,2, 10, 0, 0),
                        EndTime = new DateTime(2021, 12,2, 18, 0, 0),
                        DayOfWeek = DayOfWeek.Monday,
                        IsRegular = false,
                        MaxClients = 10
                    },
                };

                context.ScheduleDays.AddRange(entitiesList);
                await context.SaveChangesAsync();
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
                        ScheduleDayId = 1,
                        Date = DateTime.Now
                    },
                    new Reservation
                    {
                        UserId = 1,
                        Prize = 15,
                        ReservationDuration = 45,
                        ServiceCategoryId = 1,
                        ScheduleDayId = 1,
                        Date = DateTime.Now
                    }
                };

                context.Reservations.AddRange(entitiesList);
                await context.SaveChangesAsync();
            }

            if (!context.Opinions.Any())
            {
                var entitiesList = new List<Opinion>
                {
                    new Opinion
                    {
                        UserId = 1,
                        Date = DateTime.Now,
                        Content = "Good job",
                        RateValue = 4,
                        ReservationId = 1,
                    },
                    new Opinion
                    {
                        UserId = 2,
                        Date = DateTime.Now,
                        Content = "Bad job",
                        RateValue = 1,
                        ReservationId = 1,
                    }
                };

                context.Opinions.AddRange(entitiesList);
                await context.SaveChangesAsync();
            }

            if (!context.UserCompanyAccessTypes.Any())
            {
                var entitiesList = new List<UserCompanyAccessType>
                {
                    new UserCompanyAccessType
                    {
                        CreatedAt = DateTime.Now,
                        AccessLevel = 5,
                        Name = "Owner",
                    },
                    new UserCompanyAccessType
                    {
                        CreatedAt = DateTime.Now,
                        AccessLevel = 2,
                        Name = "Worker",
                    }
                };

                context.UserCompanyAccessTypes.AddRange(entitiesList);
                await context.SaveChangesAsync();
            }

            if (!context.UserCompanies.Any())
            {
                var entitiesList = new List<UserCompany>
                {
                    new UserCompany
                    {
                        UserId = 4,
                        CompanyId = 1,
                        AccessTypeId = 1,
                        CreatedAt = DateTime.Now,
                    },
                    new UserCompany
                    {
                        UserId = 3,
                        CompanyId = 1,
                        AccessTypeId = 2,
                        CreatedAt = DateTime.Now,
                    },
                };

                context.UserCompanies.AddRange(entitiesList);
                await context.SaveChangesAsync();
            }

        }
    }
}
