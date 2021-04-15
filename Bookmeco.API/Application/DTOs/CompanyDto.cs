using System.Collections.Generic;

namespace Application.DTOs
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public List<CompanyContentDto> Contents { get; set; }
        public List<string> Categories { get; set; }
        public List<int> UserIds { get; set; }
    }
}
