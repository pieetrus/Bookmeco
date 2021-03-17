using Domain.Entities;
using System.Collections.Generic;

namespace Application.Users
{
    public class UserDto
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public List<Role> Roles { get; set; }
    }
}
