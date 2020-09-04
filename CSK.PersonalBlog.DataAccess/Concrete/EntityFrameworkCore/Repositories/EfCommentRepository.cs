using CSK.PersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Context;
using CSK.PersonalBlog.DataAccess.Interfaces;
using CSK.PersonalBlog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCommentRepository : EfGenericRepository<Comment>, ICommentDal
    {
        private readonly PersonalBlogContext _context;
        public EfCommentRepository(PersonalBlogContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllWithSubCommentsAsync(int blogId, int? parentCommentId)
        {
            List<Comment> result = new List<Comment>();
            await GetComments(blogId, parentCommentId, result);
            return result;
        }

        public async Task GetComments(int blogId, int? parentCommentId, List<Comment> result)
        {
            var comments = await _context.Comments.Where(i => i.BlogId == blogId && i.ParentCommentId == parentCommentId)
                                                 .OrderByDescending(i => i.PostedTime)
                                                 .ToListAsync();

            if (comments.Count > 0)
            {
                foreach (var comment in comments)
                {
                    if (comment.SubComments == null)
                        comment.SubComments = new List<Comment>();

                    await GetComments(comment.BlogId, comment.Id, comment.SubComments);

                    if (!result.Contains(comment))
                        result.Add(comment);
                }
            }
        }
    }
}
