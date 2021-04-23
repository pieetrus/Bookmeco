using System.Collections.Generic;

namespace Application.DTOs
{
    public class UserLoginDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }
    }
}
