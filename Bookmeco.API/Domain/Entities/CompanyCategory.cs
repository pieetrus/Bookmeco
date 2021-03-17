using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class CompanyCategory : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CompanyCategory SuperCompanyCategory { get; set; }
        public List<Company> Companies { get; set; }
    }
}
