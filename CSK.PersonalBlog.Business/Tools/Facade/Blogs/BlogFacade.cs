using CSK.PersonalBlog.Business.Interface;
using Microsoft.Extensions.Caching.Memory;

namespace CSK.PersonalBlog.Business.Tools.Facade.Blogs
{
    public class BlogFacade : IBlogFacade
    {
        public IMemoryCache MemoryCache { get; set; }
        public IBlogService BlogService { get; set; }
        public ICommentService CommentService { get; set; }
        public BlogFacade(IMemoryCache memoryCache, IBlogService blogService, ICommentService commentService)
        {
            MemoryCache = memoryCache;
            BlogService = blogService;
            CommentService = commentService;
        }
    }
}
