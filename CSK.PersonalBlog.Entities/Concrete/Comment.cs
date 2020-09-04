using CSK.PersonalBlog.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace CSK.PersonalBlog.Entities.Concrete
{
    public class Comment : BaseEntity<int>, ITable
    {
        #region Properties
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Description { get; set; }
        public DateTime PostedTime { get; set; } = DateTime.Now;
        #endregion

        #region Relationships
        public int? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public List<Comment> SubComments { get; set; }
        #endregion
    }
}
