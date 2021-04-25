using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Role> Roles { get; set; }
        public List<Schedule> Schedules { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<UserCompany> UserCompanies { get; set; }
        public List<ServiceCategory> ServiceCategories { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
