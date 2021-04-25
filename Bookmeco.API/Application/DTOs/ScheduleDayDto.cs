using System;
using System.Collections.Generic;

namespace Application.DTOs
{
    public class ScheduleDayDto
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public DayOfWeek? DayOfWeek { get; set; }
        public bool IsRegular { get; set; }
        public int? MaxClients { get; set; }
        public List<int> ReservationIds { get; set; }
    }
}
