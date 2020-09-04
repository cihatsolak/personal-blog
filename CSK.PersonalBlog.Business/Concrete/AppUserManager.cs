using CSK.PersonalBlog.Business.Interface;
using CSK.PersonalBlog.DataAccess.Interfaces;
using CSK.PersonalBlog.DTO.DTOs.AppUserDtos;
using CSK.PersonalBlog.Entities.Concrete;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.Business.Concrete
{
    public class AppUserManager : GenericManager<AppUser>, IAppUserService
    {
        private readonly IAppUserDal _appUserDal;
        public AppUserManager(IGenericDal<AppUser> genericDal, IAppUserDal appUserDal) : base(genericDal)
        {
            _appUserDal = appUserDal;
        }

        public async Task<AppUser> FindByUsernameAsync(string username)
        {
            return await _appUserDal.GetAsync(i => i.UserName.Equals(username));
        }

        public async Task<AppUser> GetUserAsync(AppUserLoginDto appUserLoginDto)
        {
            return await _appUserDal.GetAsync(i => i.UserName.Equals(appUserLoginDto.UserName) && i.Password.Equals(appUserLoginDto.Password));
        }
    }
}
