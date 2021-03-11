using System.Collections.Generic;

namespace Domain.Entities
{
    public class ServiceCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ServiceProvider> ServiceProviders { get; set; }
    }
}
