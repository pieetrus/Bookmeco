using System.Collections.Generic;

namespace Application.DTOs
{
    public class RoleDto
    {
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public int AccessLevel { get; set; }
        public List<UserDto> Users { get; set; }
    }
}
