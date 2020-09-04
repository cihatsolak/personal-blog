using CSK.PersonalBlog.Entities.Interfaces;
using System.Collections.Generic;

namespace CSK.PersonalBlog.Entities.Concrete
{
    public class AppUser : BaseEntity<int>, ITable
    {
        #region Properties
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        #endregion

        #region RelationShips
        public List<Blog> Blogs { get; set; }
        public List<Log> Logs { get; set; }
        #endregion
    }
}
