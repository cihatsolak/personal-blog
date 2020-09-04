using CSK.PersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Context;
using CSK.PersonalBlog.DataAccess.Interfaces;
using CSK.PersonalBlog.Entities.Concrete;

namespace CSK.PersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfLoggerRepository : EfGenericRepository<Log>, ILoggerDal
    {
        public EfLoggerRepository(PersonalBlogContext context) : base(context)
        {

        }
    }
}
