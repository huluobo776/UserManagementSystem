using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using System.Xml;

namespace UserManagement_System.Middleware
{
    /// <summary>
    /// 中间件，异常拦截器
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DbUpdateException DBError)
            {
                _logger.LogError(DBError, "Unhandled exception");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    success = false,
                    error = "系统内部数据库错误，请联系管理员！"
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unhandled exception");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    success = false,
                    error = "系统内部错误"
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
