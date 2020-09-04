using CSK.PersonalBlog.DTO.Interfaces;

namespace CSK.PersonalBlog.DTO.DTOs.AppUserDtos
{
    public class AppUserLoginDto : IDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
