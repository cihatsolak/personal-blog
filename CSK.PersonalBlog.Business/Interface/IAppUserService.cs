using CSK.PersonalBlog.DTO.DTOs.AppUserDtos;
using CSK.PersonalBlog.Entities.Concrete;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.Business.Interface
{
    public interface IAppUserService : IGenericService<AppUser>
    {
        Task<AppUser> GetUserAsync(AppUserLoginDto appUserLoginDto);
        Task<AppUser> FindByUsernameAsync(string username);
    }
}
