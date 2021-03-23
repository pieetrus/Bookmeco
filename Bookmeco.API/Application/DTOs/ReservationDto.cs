using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public Schedule Schedule { get; set; }
        public ServiceCategory ServiceCategory { get; set; }
        public PersonData PersonData { get; set; }
        public List<Opinion> Opinions { get; set; }
        public DateTime Date { get; set; }
        public int ReservationDuration { get; set; }
        public float Prize { get; set; }
    }
}
