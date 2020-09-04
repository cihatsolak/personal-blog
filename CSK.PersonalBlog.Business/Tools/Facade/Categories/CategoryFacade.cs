using CSK.PersonalBlog.Business.Interface;
using Microsoft.Extensions.Caching.Memory;

namespace CSK.PersonalBlog.Business.Tools.Facade.Categories
{
    public class CategoryFacade : ICategoryFacade
    {
        public ICategoryService CategoryService { get; set; }
        public IMemoryCache MemoryCache { get; set; }

        public CategoryFacade(ICategoryService categoryService, IMemoryCache memoryCache)
        {
            CategoryService = categoryService;
            MemoryCache = memoryCache;
        }
    }
}
