using CSK.PersonalBlog.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace CSK.PersonalBlog.Entities.Concrete
{
    public class Blog : BaseEntity<int>, ITable
    {
        #region Properties
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime PostedTime { get; set; } = DateTime.Now;
        #endregion

        #region RelationShips
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public List<CategoryBlog> CategoryBlogs { get; set; }

        public List<Comment> Comments { get; set; }
        #endregion
    }
}
