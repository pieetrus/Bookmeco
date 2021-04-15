using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IDataContext
    {
        DbSet<Company> Companies { get; set; }
        DbSet<CompanyCategory> CompanyCategories { get; set; }
        DbSet<ServiceCategory> ServiceCategories { get; set; }
        DbSet<CompanyContent> CompanyContents { get; set; }
        DbSet<Opinion> Opinions { get; set; }
        DbSet<Reservation> Reservations { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Schedule> Schedules { get; set; }
        DbSet<ScheduleDay> ScheduleDays { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserCompany> UserCompanies { get; set; }
        DbSet<UserCompanyAccessType> UserCompanyAccessTypes { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
