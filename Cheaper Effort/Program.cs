using Cheaper_Effort.Data;
using Cheaper_Effort.Middlewares;
using Cheaper_Effort.Models;

using Cheaper_Effort.Serivces;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

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
    builder.Services.AddTransient<IUserService, UserService>();
    builder.Services.AddTransient<INewRecipeService, NewRecipeService>();
    builder.Services.AddTransient<IRecipeService, RecipeService>();
    builder.Services.AddDbContext<ProjectDbContext>(o => o.UseSqlite("filename=Data/Database/Project.db"));
    builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
    {

        options.Cookie.Name = "MyCookieAuth";

    });

    //builder.Services.AddEndpointsApiExplorer();
    //builder.Services.AddSwaggerGen();
    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();
    //app.UseSwaggerUI();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");

        app.UseHsts();
    }

    //app.UseSwagger(x => x.SerializeAsV2 = true);

    app.UseDateLogMiddleware();

    app.UseBrowserMiddleware();

    app.UseStatusCodePages();

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapRazorPages();

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