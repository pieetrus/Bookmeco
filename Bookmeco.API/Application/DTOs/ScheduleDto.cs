using System.Collections.Generic;

namespace Application.DTOs
{
    public class ScheduleDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public bool IsAvailable { get; set; }
        public List<int> ScheduleDayIds { get; set; }
        public List<int> ReservationIds { get; set; }
    }
}
