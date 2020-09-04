using CSK.PersonalBlog.Business.Interface;
using Microsoft.Extensions.Caching.Memory;

namespace CSK.PersonalBlog.Business.Tools.Facade.Categories
{
    public interface ICategoryFacade
    {
        public Interface.ICategoryService CategoryService { get; set; }
        public IMemoryCache MemoryCache { get; set; }
    }
}
