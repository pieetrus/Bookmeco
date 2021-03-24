namespace Application.DTOs
{
    public class UserCompanyDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public UserCompanyAccessTypeDto AccessType { get; set; }
    }
}
