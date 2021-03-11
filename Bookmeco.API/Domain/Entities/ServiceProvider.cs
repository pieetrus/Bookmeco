using System.Collections.Generic;

namespace Domain.Entities
{
    public class ServiceProvider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ServiceCategory> ServiceCategory { get; set; }    
    }
}
