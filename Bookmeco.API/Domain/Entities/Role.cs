using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    /// <summary>
    /// Role has a name (for users) and AccessLevel.
    /// If accessLevel is grater than require level then access allowed.
    /// Admin, std User, System access only.
    /// Each user is allow to has many roles. 
    /// </summary>
    public class Role : IdentityRole<int>
    {
        public int AccessLevel { get; set; }
        public List<User> Users { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
