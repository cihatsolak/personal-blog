using CSK.PersonalBlog.Business.StringInfos;
using CSK.PersonalBlog.Business.Tools.Enums;
using CSK.PersonalBlog.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.WebApi.Controllers
{
    [Route("api/v1/[controller]"), ApiController, Authorize, EnableCors]
    public class BaseApiController : ControllerBase
    {
        public IActionResult RESPONSE(StatusCodeType statusCodeType, string statusMessage, ResultType resultType, object result = null)
        {
            var model = new ResponseModel<object>
            {
                StatusCode = (Int32)statusCodeType,
                StatusMessage = statusMessage,
                Result = result
            };

            return resultType switch
            {
                ResultType.Ok => Ok(model),
                ResultType.Created => Created(string.Empty, model),
                ResultType.NoContent => NoContent(),
                ResultType.NotFound => NotFound(model),
                ResultType.BadRequest => BadRequest(model),
                _ => Ok(model),
            };
        }


        [NonAction]
        protected async Task<UploadModel> UploadFileAsync(IFormFile file, string contentType)
        {
            var uploadModel = new UploadModel();

            if (file != null)
            {
                if (file.ContentType.Equals(contentType))
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/img/{fileName}");

                    using var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);

                    uploadModel.NewName = fileName;
                    uploadModel.UploadState = UploadState.Success;
                    return uploadModel;
                }
                else
                {
                    uploadModel.ErrorMessage = StatusMessage.INVALID_FILE_TYPE;
                    uploadModel.UploadState = UploadState.Error;
                    return uploadModel;
                }
            }

            uploadModel.ErrorMessage = StatusMessage.FILE_NOT_FOUND;
            uploadModel.UploadState = UploadState.NotExist;
            return uploadModel;
        }
    }
}
