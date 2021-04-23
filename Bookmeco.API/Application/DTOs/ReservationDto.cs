using System;

namespace Application.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int ServiceCategoryId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int ReservationDuration { get; set; }
        public float Prize { get; set; }
    }
}
