using CSK.PersonalBlog.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.DataAccess.Interfaces
{
    public interface IBlogDal : IGenericDal<Blog>
    {
        Task<List<Blog>> GetAllByCategoryIdAsync(int categoryId);
        Task<List<Category>> GetCategoriesByIdAsync(int id);
        Task<List<Blog>> GetLastFiveBlogAsync();
    }
}
