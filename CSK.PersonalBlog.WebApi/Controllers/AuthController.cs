using AutoMapper;
using CSK.PersonalBlog.Business.StringInfos;
using CSK.PersonalBlog.Business.Tools.Enums;
using CSK.PersonalBlog.Business.Tools.Facade.Auth;
using CSK.PersonalBlog.DTO.DTOs.AppUserDtos;
using CSK.PersonalBlog.WebApi.CustomFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.WebApi.Controllers
{
    public class AuthController : BaseApiController
    {
        #region Fields
        private readonly IAuthFacade _authFacadeService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public AuthController(IAuthFacade authFacadeService, IMapper mapper)
        {
            _mapper = mapper;
            _authFacadeService = authFacadeService;
        }
        #endregion

        #region Action Methods

        [AllowAnonymous, HttpPost("[action]"), ValidateModel]
        public async Task<IActionResult> SignIn(AppUserLoginDto appUserLoginDto)
        {
            var user = await _authFacadeService.AppUserService.GetUserAsync(appUserLoginDto);

            if (user == null)
                return RESPONSE(StatusCodeType.USR_OR_PASS_ERR, StatusMessage.USR_OR_PASS_ERR, ResultType.NotFound);

            var token = _authFacadeService.JwtService.GenerateJwt(user);

            return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.Created, token);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ActiveUser()
        {
            var user = await _authFacadeService.AppUserService.FindByUsernameAsync(User.Identity.Name);

            if (user == null)
                return RESPONSE(StatusCodeType.UNKNOWN_ERROR, StatusMessage.UNKNOWN_ERROR, ResultType.BadRequest);

            var appUserDto = _mapper.Map<AppUserDto>(user);

            return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.Ok, appUserDto);
        }
        #endregion
    }
}
