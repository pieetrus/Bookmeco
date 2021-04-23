using System.Collections.Generic;

namespace Application.DTOs
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AccessLevel { get; set; }
        public List<int> UserIds { get; set; }
    }
}
