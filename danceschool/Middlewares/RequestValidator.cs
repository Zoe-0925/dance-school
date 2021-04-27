using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace danceschool.Middlewares
{
    public class RequestValidator
    {
        private readonly RequestDelegate _next;

        public RequestValidator(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            string token = context.Request.Headers["Authorization"];

            if (token == null || token == "")
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Access denied!");
                return;
            }

            await _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RequestValidatorExtensions
    {
        public static IApplicationBuilder UseRequestValidator(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestValidator>();
        }
    }
}