// See https://aka.ms/new-console-template for more information
using Castle.Core;
using Castle.DynamicProxy;
using Castle.Windsor;
//using log4net;
//using log4net.Config;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Component = Castle.MicroKernel.Registration.Component;


var container = new WindsorContainer()
    .Register(
        Component.For<DurationInterceptor>(),
        Component.For<IDelayer>().ImplementedBy<Delayer>()
            .Interceptors(new InterceptorReference(typeof(DurationInterceptor))).First
    );
var sender = container.Resolve<IDelayer>();

sender.Delay(1000);
sender.Delay(2000);


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

        Console.WriteLine(str);
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


