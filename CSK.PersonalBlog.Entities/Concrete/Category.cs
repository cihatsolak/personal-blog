using CSK.PersonalBlog.Entities.Interfaces;
using System.Collections.Generic;

namespace CSK.PersonalBlog.Entities.Concrete
{
    public class Category : BaseEntity<int>, ITable
    {
        #region Properties
        public string Name { get; set; }
        #endregion

        #region RelationShips
        public List<CategoryBlog> CategoryBlogs { get; set; }
        #endregion
    }
}
