using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    /// <summary>
    /// Reservation connects person data(guest allows) and schedule.
    /// Not schedule Day because there are regular days.
    /// </summary>
    public class Reservation
    {
        public int Id { get; set; }
        public int ScheduleDayId { get; set; }
        public ScheduleDay ScheduleDay { get; set; }
        public int ServiceCategoryId { get; set; }
        public ServiceCategory ServiceCategory { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Opinion> Opinions { get; set; }
        public DateTime Date { get; set; }

        /// <summary>
        /// Default Reservation Duration for service is read from ServiceCategory table.
        /// If some worker want to change prize for this one reservation he can do it here.
        /// </summary>
        public int ReservationDuration { get; set; }

        /// <summary>
        /// Default Prize for service is read from ServiceCategory table.
        /// If some worker want to change prize for this one reservation he can do it here.
        /// </summary>
        public float Prize { get; set; }
    }
}
