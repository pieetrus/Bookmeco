using Domain.Entities;
using System.Collections.Generic;

namespace Application.DTOs
{
    public class ScheduleDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsAvailable { get; set; }
        public List<ScheduleDay> ScheduleDays { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
