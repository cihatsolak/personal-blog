using CSK.PersonalBlog.Business.Interface;
using CSK.PersonalBlog.DataAccess.Interfaces;
using CSK.PersonalBlog.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.Business.Concrete
{
    public class CommentManager : GenericManager<Comment>, ICommentService
    {
        private readonly ICommentDal _commentDal;
        public CommentManager(IGenericDal<Comment> genericDal, ICommentDal commentDal) : base(genericDal)
        {
            _commentDal = commentDal;
        } 

        public async Task<List<Comment>> GetAllWithSubCommentsAsync(int blogId, int? parentCommentId)
        {
            return await _commentDal.GetAllWithSubCommentsAsync(blogId, parentCommentId);
        }
    }
}
