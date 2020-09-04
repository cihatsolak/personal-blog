using CSK.PersonalBlog.DTO.Interfaces;

namespace CSK.PersonalBlog.DTO.DTOs.CategoryDtos
{
    public class CategoryUpdateDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
