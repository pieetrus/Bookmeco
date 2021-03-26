namespace Application.DTOs
{
    public class CompanyContentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int CompanyId { get; set; }
        public CompanyDto Company { get; set; }
    }
}
