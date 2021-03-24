using Domain.Common;

namespace Domain.Entities
{
    /// <summary>
    /// Splitted person data and user allow sto create guest account.
    /// Person data are used to reservations.
    /// User is used only to account management.(log in, log out)
    /// </summary>
    public class PersonData : AuditableEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
