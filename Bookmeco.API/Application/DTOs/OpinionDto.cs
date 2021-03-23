using System;

namespace Application.DTOs
{
    public class OpinionDto
    {
        public int Id { get; set; }
        public ReservationDto Reservation { get; set; }
        public OpinionDto SuperOpinion { get; set; }
        public UserDto User { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int? RateValue { get; set; }
    }
}
