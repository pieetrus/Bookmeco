using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Category : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category SuperCategory { get; set; }
        public List<Company> Companies { get; set; }
    }
}
