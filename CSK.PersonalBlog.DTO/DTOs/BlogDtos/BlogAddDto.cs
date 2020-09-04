using CSK.PersonalBlog.DTO.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CSK.PersonalBlog.DTO.DTOs.BlogDtos
{
    public class BlogAddDto : IDto
    {
        public int AppUserId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
