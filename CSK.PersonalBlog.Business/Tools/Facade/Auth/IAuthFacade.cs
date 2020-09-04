using CSK.PersonalBlog.Business.Interface;
using CSK.PersonalBlog.Business.Tools.JWT;

namespace CSK.PersonalBlog.Business.Tools.Facade.Auth
{
    public interface IAuthFacade
    {
        public IAppUserService AppUserService { get; set; }
        public IJwtService JwtService { get; set; }
    }
}
