using System;

namespace Domain.Entities
{
    /// <summary>
    /// If is Regular is not then date has to be set and define which date schedule day referes to. e.x.There are holidays from 1.07.21 to 14.07.21
    /// Describes when the company has open. Days and hours.
    /// If is regular then each day (ref. day enum) has the same hours. e.x.
    /// Each monday the company has open from 9:00 to 18:00
    /// </summary>
    public class ScheduleDay
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public Schedule Schedule { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime Date { get; set; }
        public bool IsRegular { get; set; }
        public int? MaxClients { get; set; }
        public float Prize { get; set; }
        public int Time { get; set; }
    }
}
