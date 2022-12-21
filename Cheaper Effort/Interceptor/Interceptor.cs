using System;
using System.Reflection;
using System.Web.Http.Filters;
using System.Net;
using System.Web.Http.Controllers;
using Castle.Core;
using Castle.DynamicProxy;
using Castle.Windsor;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using NLog.Web;
using NLog.Fluent;

namespace Cheaper_Effort.Interceptor

{

    public class DurationInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {

            var stopwatch = Stopwatch.StartNew();

            invocation.Proceed();

            stopwatch.Stop();

            string str = string.Format(
                "DurationInterceptor: {0} executed in {1} milliseconds.",
                invocation.MethodInvocationTarget.Name,
                stopwatch.Elapsed.TotalMilliseconds.ToString("0.000")
            );

            Log.Debug(str);
        }
    }

    public interface IDelayer
    {
        public void Delay(int ms);
    }

    public class Delayer : IDelayer
    {
        public void Delay(int ms)
        {
            Thread.Sleep(ms);
        }

    }
}
    
   

