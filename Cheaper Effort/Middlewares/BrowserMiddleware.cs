using System.Diagnostics;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NuGet.Protocol.Core.Types;

namespace Cheaper_Effort.Middlewares
{
    public class BrowserMiddleware
    {
        private readonly RequestDelegate _next;

        public BrowserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, ILogger<DateLogMiddleware> logger)
        {
            try
            {
                var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
                var ipAddress = httpContext.Connection.RemoteIpAddress.ToString();
                var url = httpContext.Request.Path;
                logger.LogInformation("userAgent: " + userAgent);
                logger.LogInformation("ipAddress: " + ipAddress);
                logger.LogInformation("url: " + url);
            }
            catch
            {
                var url = httpContext.Request.Path;
                logger.LogInformation("userAgent: not found");
                logger.LogInformation("ipAddress: not found");
                logger.LogInformation("url: " + url);
            }
            //Debug.WriteLine("userAgent: " + userAgent);
            //Debug.WriteLine("ipAddress: " + ipAddress);
            //Debug.WriteLine("url: " + url);
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
