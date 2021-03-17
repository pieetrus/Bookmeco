using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User : AuditableEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Role> Roles { get; set; }
        public List<ServiceCategory> ServiceCategories { get; set; }
    }
}
