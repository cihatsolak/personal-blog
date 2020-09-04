using CSK.PersonalBlog.Business.Interface;
using CSK.PersonalBlog.Entities.Concrete;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace CSK.PersonalBlog.WebApi.Controllers
{
    public class LogsController : BaseApiController
    {
        #region Fields
        private readonly ILoggerService _loggerService;
        #endregion

        #region Constructor
        public LogsController(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }
        #endregion

        #region Action Methods
        [HttpGet("[action]")]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            InsertLog(exception);
            return Problem(detail: "Belirlenemeyen bir hata oluştu. Log kayıtlarını inceleyiniz.");
        }
        #endregion

        #region NonAction Methods
        [NonAction]
        private void InsertLog(IExceptionHandlerPathFeature exception)
        {
            //Insert Database
            _loggerService.InsertAsync(new Log
            {
                AppUserId = int.Parse(User.Claims.Single(i => i.Type.Equals(ClaimTypes.NameIdentifier))?.Value),
                ErrorMessage = exception.Error.Message,
                Path = exception.Path,
                StackTrace = exception.Error.StackTrace
            });

            //Insert NLog
            string ex = $"Hatanın oluştuğu yer:{exception.Path}";
            ex += $"Hata Mesajı: {exception.Error.Message}";
            ex += $"Stack Trace: {exception.Error.StackTrace}";
            _loggerService.LogError(ex);
        }
        #endregion
    }
}
