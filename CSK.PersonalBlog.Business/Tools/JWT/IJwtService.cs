using CSK.PersonalBlog.Entities.Concrete;

namespace CSK.PersonalBlog.Business.Tools.JWT
{
    public interface IJwtService
    {
        JwtToken GenerateJwt(AppUser appUser);
    }
}
