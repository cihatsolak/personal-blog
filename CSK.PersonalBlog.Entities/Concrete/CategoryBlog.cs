using CSK.PersonalBlog.Entities.Interfaces;

namespace CSK.PersonalBlog.Entities.Concrete
{
    public class CategoryBlog : BaseEntity<int>, ITable
    {
        #region RelationShips
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        #endregion
    }
}
