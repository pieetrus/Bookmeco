using System.Collections.Generic;

namespace Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public List<int> ServiceCategoriesIds { get; set; }
        public List<int> ScheduleIds { get; set; }
        public List<int> ReservationIds { get; set; }
    }
}
