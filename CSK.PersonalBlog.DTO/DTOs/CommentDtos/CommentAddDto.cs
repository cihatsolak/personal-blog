using CSK.PersonalBlog.DTO.Interfaces;
using System;

namespace CSK.PersonalBlog.DTO.DTOs.CommentDtos
{
    public class CommentAddDto : IDto
    {
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Description { get; set; }
        public DateTime PostedTime { get; set; } = DateTime.Now;

        public int? ParentCommentId { get; set; }
        public int BlogId { get; set; }
    }
}
