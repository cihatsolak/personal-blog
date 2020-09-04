using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.WebUI.Models.Comments
{
    public class CommentAddViewModel
    {
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Description { get; set; }
        public DateTime PostedTime { get; set; } = DateTime.Now;

        public int? ParentCommentId { get; set; }
        public int BlogId { get; set; }
    }
}
