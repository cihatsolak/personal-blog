using CSK.PersonalBlog.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.Business.Interface
{
    public interface IBlogService : IGenericService<Blog>
    {
        Task<List<Blog>> GetAllSortedByPostedTimeAsync();
        Task InsertFromCategoryAsync(CategoryBlog categoryBlog);
        Task DeleteFromCategoryAsync(CategoryBlog categoryBlog);
        Task<List<Blog>> GetAllByCategoryIdAsync(int categoryId);
        Task<List<Category>> GetCategoriesByBlogIdAsync(int id);
        Task<List<Blog>> GetLastFiveBlogAsync();
        Task<List<Blog>> GetAllSearchByPostedTimeAsync(string searchKeyword);
    }
}
