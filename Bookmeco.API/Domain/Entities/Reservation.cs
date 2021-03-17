using System;

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
        public PersonData PersonData { get; set; }
        public DateTime Date { get; set; }
        public int ReservationTime { get; set; }
    }
}
