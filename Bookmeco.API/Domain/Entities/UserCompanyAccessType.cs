using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    /// <summary>
    /// User role in company like stuff (can make and remove reservations), owner (can change content).
    /// Access only to company specific parameters.
    /// </summary>
    public class UserCompanyAccessType : AuditableEntity
    {
        public int Id { get; set; }
        public int AccessLevel { get; set; }
        public string Name { get; set; }
        public List<UserCompany> UserCompanies { get; set; }
    }
}
