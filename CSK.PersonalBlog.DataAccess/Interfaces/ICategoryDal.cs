using CSK.PersonalBlog.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.DataAccess.Interfaces
{
    public interface ICategoryDal : IGenericDal<Category>
    {
        Task<List<Category>> GetAllWithCategoryBlogsAsync();
    }
}
