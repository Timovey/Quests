using CommonInfrastructure.Http;
using System.Globalization;

namespace QuestCore.Middleware
{
    public class AddUserMiddleware
    {
        private readonly RequestDelegate _next;

        public AddUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task<CommonHttpRequest> InvokeAsync(HttpContext context)
        {
            int i = 0;
            return new CommonHttpRequest();
        }
        public async Task<CommonHttpRequest> InvokeAsync(CommonHttpRequest request)
        {
            int i = 0;
            return request;
        }
    }
    public static class AddUserMiddlewareExtensions
    {
        public static IApplicationBuilder UseAddUser(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AddUserMiddleware>();
        }
    }
}
