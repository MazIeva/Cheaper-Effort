using System.Diagnostics;

namespace Cheaper_Effort.Middlewares
{
    public class ElapsedTimeMiddleware
    {
        private readonly RequestDelegate _next;
        public ElapsedTimeMiddleware(RequestDelegate next) => _next = next;
        public async Task Invoke(HttpContext context, ILogger<ElapsedTimeMiddleware> logger)
        {
            var sw = new Stopwatch();
            sw.Start();
            await _next(context);
            var isHtml = context.Response.ContentType?.ToLower().Contains("text/html");
            if (context.Response.StatusCode == 200 && isHtml.GetValueOrDefault())
            {
                logger.LogInformation($"{context.Request.Path} executed in {sw.ElapsedMilliseconds}ms");
            }
        }
    }

    public static class ElapsedTimeMiddlewareExtentions
    {
        public static IApplicationBuilder UseElapsedTimeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ElapsedTimeMiddleware>();
        }
    }

}
