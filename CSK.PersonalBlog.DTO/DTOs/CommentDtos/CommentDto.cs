using CSK.PersonalBlog.DTO.Interfaces;
using System;
using System.Collections.Generic;

namespace CSK.PersonalBlog.DTO.DTOs.CommentDtos
{
    public class CommentDto : IDto
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Description { get; set; }
        public DateTime PostedTime { get; set; }

        public int? ParentCommentId { get; set; }
        public List<CommentDto> SubComments { get; set; }
        public int BlogId  { get; set; }
    }
}
