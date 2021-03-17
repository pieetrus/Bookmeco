using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ServiceCategory : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Prize { get; set; }
        public int ServiceDuration { get; set; }
        public List<User> Users { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
