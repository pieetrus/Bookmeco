using Domain.Common;

namespace Domain.Entities
{
    /// <summary>
    /// Each user is allowed to has access to more companies.
    /// He or she can be owner of camp. A, moderator or stuff in other. 
    /// </summary>
    public class UserCompany : AuditableEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public UserCompanyAccessType AccessType { get; set; }
    }
}
