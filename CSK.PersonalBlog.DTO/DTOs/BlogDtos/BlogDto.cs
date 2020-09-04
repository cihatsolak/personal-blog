using CSK.PersonalBlog.DTO.Interfaces;
using System;

namespace CSK.PersonalBlog.DTO.DTOs.BlogDtos
{
    public class BlogDto : IDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime PostedTime { get; set; }
    }
}
