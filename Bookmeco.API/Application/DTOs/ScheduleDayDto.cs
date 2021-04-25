using System;

namespace Application.DTOs
{
    public class ScheduleDayDto
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        /// <summary>
        /// Seconds past 0:00
        /// </summary>
        public int BeginTime { get; set; }
        /// <summary>
        /// Seconds past 0:00
        /// </summary>
        public int EndTime { get; set; }
        public DayOfWeek? DayOfWeek { get; set; }
        public DateTime? Date { get; set; }
        public bool IsRegular { get; set; }
        public int? MaxClients { get; set; }
    }
}
