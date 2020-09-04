using System;
using System.Collections.Generic;

namespace CSK.PersonalBlog.WebUI.Models.Comments
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Description { get; set; }
        public DateTime PostedTime { get; set; }

        public int? ParentCommentId { get; set; }
        public List<CommentViewModel> SubComments { get; set; }
        public int BlogId { get; set; }
    }
}
