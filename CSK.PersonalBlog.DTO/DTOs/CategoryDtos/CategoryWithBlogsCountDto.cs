using CSK.PersonalBlog.DTO.Interfaces;

namespace CSK.PersonalBlog.DTO.DTOs.CategoryDtos
{
    public class CategoryWithBlogsCountDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BlogsCount { get; set; }
    }
}
