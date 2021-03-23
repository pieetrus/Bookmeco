using Domain.Entities;
using System.Collections.Generic;

namespace Application.DTOs
{
    public class CompanyCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CompanyCategory SuperCompanyCategory { get; set; }
        public List<CompanyDto> Companies { get; set; }
    }
}
