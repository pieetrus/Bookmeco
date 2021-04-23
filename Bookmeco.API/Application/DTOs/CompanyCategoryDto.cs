using System.Collections.Generic;

namespace Application.DTOs
{
    public class CompanyCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? SuperCompanyCategoryId { get; set; }
        public List<int> CompanyIds { get; set; }
    }
}
