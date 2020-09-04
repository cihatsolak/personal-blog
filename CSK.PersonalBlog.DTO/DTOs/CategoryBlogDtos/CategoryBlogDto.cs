using CSK.PersonalBlog.DTO.Interfaces;

namespace CSK.PersonalBlog.DTO.DTOs.CategoryBlogDtos
{
    public class CategoryBlogDto : IDto
    {
        public int BlogId { get; set; }
        public int CategoryId { get; set; }
    }
}
