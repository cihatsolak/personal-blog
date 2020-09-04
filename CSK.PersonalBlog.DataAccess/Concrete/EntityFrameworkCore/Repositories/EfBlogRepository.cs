using CSK.PersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Context;
using CSK.PersonalBlog.DataAccess.Interfaces;
using CSK.PersonalBlog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfBlogRepository : EfGenericRepository<Blog>, IBlogDal
    {
        private readonly PersonalBlogContext _context;
        public EfBlogRepository(PersonalBlogContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Blog>> GetAllByCategoryIdAsync(int categoryId)
        {
            var query = _context.Blogs.Join(_context.CategoryBlogs, blog => blog.Id, category => category.BlogId, (blog, categoryBlog) => new
            {
                tblBlog = blog,
                tblCategoryBlog = categoryBlog
            })
            .Where(i => i.tblCategoryBlog.CategoryId == categoryId)
            .Select(x => new Blog
            {
                AppUser = x.tblBlog.AppUser,
                AppUserId = x.tblBlog.AppUserId,
                CategoryBlogs = x.tblBlog.CategoryBlogs,
                Comments = x.tblBlog.Comments,
                Description = x.tblBlog.Description,
                Id = x.tblBlog.Id,
                ImagePath = x.tblBlog.ImagePath,
                PostedTime = x.tblBlog.PostedTime
            });

            return await query.ToListAsync();
        }

        public async Task<List<Category>> GetCategoriesByIdAsync(int id)
        {
            var query = _context.Categories.Join(_context.CategoryBlogs, category => category.Id, categoryBlogs => categoryBlogs.CategoryId, (category, categoryBlogs) => new
            {
                tblCategory = category,
                tblCategoryBlogs = categoryBlogs
            })
            .Where(i => i.tblCategoryBlogs.BlogId == id)
            .Select(i => new Category
            {
                Id = i.tblCategory.Id,
                Name = i.tblCategory.Name
            });

            return await query.ToListAsync();
        }

        public async Task<List<Blog>> GetLastFiveBlogAsync()
        {
            return await _context.Blogs.OrderByDescending(i => i.PostedTime).Take(5).ToListAsync();
        }
    }
}
