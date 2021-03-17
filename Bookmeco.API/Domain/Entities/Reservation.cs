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
        public Schedule Schedule { get; set; }
        public ServiceCategory ServiceCategory { get; set; }
        public PersonData PersonData { get; set; }
        public List<Opinion> Opinions { get; set; }
        public DateTime Date { get; set; }
        public int ReservationDuration { get; set; }
        /// <summary>
        /// Default prize for service is read from ServiceCategory table.
        /// If some worker want to change prize for this one reservation he can do it here.
        /// </summary>
        public float Prize { get; set; }
    }
}
