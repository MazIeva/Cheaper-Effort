using System.ComponentModel;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Cheaper_Effort.Data;
using Cheaper_Effort.Interceptor;
using Cheaper_Effort.Middlewares;
using Cheaper_Effort.Models;
using Autofac.Extensions.DependencyInjection;
using Cheaper_Effort.Serivces;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using Component = System.ComponentModel.Component;
using Autofac;
using NLog.Fluent;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");


try
{
    // set the envirnment to production 
    var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
    {
        EnvironmentName = Environments.Production
    });


    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddControllers();
    builder.Services.AddTransient<IUserService, UserService>();
    builder.Services.AddTransient<INewRecipeService, NewRecipeService>();
    builder.Services.AddTransient<IRecipeService, RecipeService>();
    builder.Services.AddDbContext<ProjectDbContext>(o => o.UseSqlite("filename=Data/Database/Project.db"));
    builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
    {

        options.Cookie.Name = "MyCookieAuth";

    });

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    void ConfigureContainer (ContainerBuilder builder)
    {
        builder.RegisterType<RecipeService>().As<IRecipeService>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(DurationInterceptor))
                .InstancePerDependency();

        builder.RegisterType<RecipeService>().As<IRecipeService>().InstancePerDependency();
        builder.RegisterType<DurationInterceptor>().SingleInstance();
    }
        

    builder.Host.UseNLog();

    

    var app = builder.Build();
    //app.UseSwaggerUI();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");

        app.UseHsts();
    }


    app.UseDateLogMiddleware();

    app.UseBrowserMiddleware();

    app.UseElapsedTimeMiddleware();

    app.UseStatusCodePages();

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapRazorPages();
    app.MapControllers();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapRazorPages();
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Base}/{action=Index}/{id?}");
    });

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}