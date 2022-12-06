﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Cheaper_Effort.Middlewares
{
    public class DateLogMiddleware
    {
        private readonly RequestDelegate _next;

        public DateLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, ILogger<DateLogMiddleware> logger)
        {
            logger.LogInformation("Date: " + DateTime.Now.ToLongDateString());
            //Debug.WriteLine("Date: " + DateTime.Now.ToLongDateString());
            return _next(httpContext);
        }
    }

    public static class DateLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseDateLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DateLogMiddleware>();
        }
    }
}

