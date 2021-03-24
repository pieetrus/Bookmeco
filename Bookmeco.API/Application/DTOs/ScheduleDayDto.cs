using System;

namespace Application.DTOs
{
    public class ScheduleDayDto
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public ScheduleDto Schedule { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime? Date { get; set; }
        public bool IsRegular { get; set; }
        public int? MaxClients { get; set; }
    }
}
