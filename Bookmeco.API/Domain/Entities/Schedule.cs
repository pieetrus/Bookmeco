using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    /// <summary>
    /// Company can prepare schedules for each hired worker.
    /// </summary>
    public class Schedule : AuditableEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsAvailable { get; set; }
        public ScheduleDay ScheduleDay { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
