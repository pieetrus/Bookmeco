using System.Collections.Generic;

namespace Application.DTOs
{
    public class ServiceCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Prize { get; set; }
        public int ServiceDuration { get; set; }
        public List<int> UserIds { get; set; }
    }
}
