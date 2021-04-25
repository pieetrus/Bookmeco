namespace Application.DTOs
{
    public class UserCompanyDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int AccessTypeId { get; set; }
        public UserCompanyAccessTypeDto AccessType { get; set; }
    }
}
