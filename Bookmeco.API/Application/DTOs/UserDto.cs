﻿using System.Collections.Generic;

namespace Application.DTOs
{
    public class UserDto
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public List<string> Roles { get; set; }
    }
}
