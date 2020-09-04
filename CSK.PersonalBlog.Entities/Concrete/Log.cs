using CSK.PersonalBlog.Entities.Interfaces;
using System;

namespace CSK.PersonalBlog.Entities.Concrete
{
    public class Log : BaseEntity<int>, ITable
    {
        #region Properties
        public string Path { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        #endregion

        #region RelationShips
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        #endregion
    }
}
