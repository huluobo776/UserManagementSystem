using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Runtime;
using Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace UserManagementSystemv2.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                context.Result = new BadRequestObjectResult(Result<string>.Fail(string.Join(";", errors)));
            }
            // 在这里添加验证逻辑
        }
        public void OnActionExecuted(ActionExecutedContext context) { }


    }    
}
