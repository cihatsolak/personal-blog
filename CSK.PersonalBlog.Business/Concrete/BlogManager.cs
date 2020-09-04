using CSK.PersonalBlog.Business.Interface;
using CSK.PersonalBlog.DataAccess.Interfaces;
using CSK.PersonalBlog.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.Business.Concrete
{
    public class BlogManager : GenericManager<Blog>, IBlogService
    {
        private readonly IGenericDal<CategoryBlog> _genericCategoryBlogDal;
        private readonly IBlogDal _blogDal;
        public BlogManager(
            IGenericDal<Blog> genericBlogDal,
            IGenericDal<CategoryBlog> genericCategoryBlogDal,
            IBlogDal blogDal) : base(genericBlogDal)
        {
            _genericCategoryBlogDal = genericCategoryBlogDal;
            _blogDal = blogDal;
        }

        public async Task<List<Blog>> GetAllSortedByPostedTimeAsync()
        {
            return await _blogDal.GetAllAsync(i => i.PostedTime);
        }

        public async Task InsertFromCategoryAsync(CategoryBlog categoryBlog)
        {
            var isThereCategory = await _genericCategoryBlogDal.GetAsync(i => i.BlogId == categoryBlog.BlogId && i.CategoryId == categoryBlog.CategoryId);

            if (isThereCategory == null)
                await _genericCategoryBlogDal.InsertAsync(categoryBlog);
        }

        public async Task DeleteFromCategoryAsync(CategoryBlog categoryBlog)
        {
            var isThereCategory = await _genericCategoryBlogDal.GetAsync(i => i.BlogId == categoryBlog.BlogId && i.CategoryId == categoryBlog.CategoryId);

            if (isThereCategory != null)
                await _genericCategoryBlogDal.DeleteAsync(isThereCategory);
        }

        public async Task<List<Blog>> GetAllByCategoryIdAsync(int categoryId)
        {
            return await _blogDal.GetAllByCategoryIdAsync(categoryId);
        }

        public async Task<List<Category>> GetCategoriesByBlogIdAsync(int id)
        {
            return await _blogDal.GetCategoriesByIdAsync(id);
        }

        public async Task<List<Blog>> GetLastFiveBlogAsync()
        {
            return await _blogDal.GetLastFiveBlogAsync();
        }

        public async Task<List<Blog>> GetAllSearchByPostedTimeAsync(string searchKeyword)
        {
            return await _blogDal.GetAllAsync(i => i.Title.ToLower().Contains(searchKeyword) ||
                                                   i.ShortDescription.ToLower().Contains(searchKeyword) ||
                                                   i.Description.ToLower().Contains(searchKeyword), i => i.PostedTime);
        }
    }
}
