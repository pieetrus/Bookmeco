using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    /// <summary>
    /// Company short description.
    /// Location, address, comp.name and so on.
    /// </summary>
    public class Company : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public List<CompanyContent> Contents { get; set; }
        public List<CompanyCategory> Categories { get; set; }
        public List<UserCompany> UserCompanies { get; set; }
    }
}
