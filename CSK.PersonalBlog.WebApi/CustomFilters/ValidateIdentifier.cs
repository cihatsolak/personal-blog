using CSK.PersonalBlog.Business.Interface;
using CSK.PersonalBlog.Business.StringInfos;
using CSK.PersonalBlog.Business.Tools.Enums;
using CSK.PersonalBlog.Entities.Interfaces;
using CSK.PersonalBlog.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace CSK.PersonalBlog.WebApi.CustomFilters
{
    public class ValidateIdentifier<TEntity> : IActionFilter where TEntity : class, ITable, new()
    {
        private readonly IGenericService<TEntity> _genericService;
        public ValidateIdentifier(IGenericService<TEntity> genericService)
        {
            _genericService = genericService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var keyValuePair = context.ActionArguments.FirstOrDefault(i => i.Key.Equals("id"));
            int checkedId = (int)keyValuePair.Value;

            var entity = _genericService.GetByIdAsync(checkedId).Result;

            if (entity == null)
            {
                context.Result = new NotFoundObjectResult(new ResponseModel<object>
                {
                    StatusCode = (Int32)StatusCodeType.NOT_FOUND,
                    StatusMessage = StatusMessage.NOT_FOUND 
                });
            }
        }
    }
}
