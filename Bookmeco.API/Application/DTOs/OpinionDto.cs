using System;

namespace Application.DTOs
{
    public class OpinionDto
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int? SuperOpinionId { get; set; }
        public UserDto User { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int? RateValue { get; set; }
    }
}
