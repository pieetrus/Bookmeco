using Domain.Common;

namespace Domain.Entities
{
    /// <summary>
    /// This is a piece of text (it can contain images as base64) which can be displayed on various screens.
    /// For example company can define short Description, long description, their overview and so on.
    /// Something like blog is also possible. The name will define where this content should be put.
    /// We can also predefine some names. As mentioned before short, long descriptions and so on.
    /// Blog management: if name starts with 'blog_'and with 'title' then it should be blog post titled 'title'. E.x. 'blog_first post'.
    /// </summary>
    public class CompanyContent : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
