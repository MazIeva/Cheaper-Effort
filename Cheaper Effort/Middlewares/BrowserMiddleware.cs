using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Cheaper_Effort.Middlewares
{
    public class BrowserMiddleware
    {
        private readonly RequestDelegate _next;

        public BrowserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
            var ipAddress = httpContext.Connection.RemoteIpAddress.ToString();
            var url = httpContext.Request.Path;
            Debug.WriteLine("userAgent: " + userAgent);
            Debug.WriteLine("ipAddress: " + ipAddress);
            Debug.WriteLine("url: " + url);
            return _next(httpContext);
        }
    }

    public static class BrowserMiddlewareExtensions
    {
        public static IApplicationBuilder UseBrowserMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BrowserMiddleware>();
        }
    }
}
