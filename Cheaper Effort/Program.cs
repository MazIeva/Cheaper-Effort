using Cheaper_Effort.Data;
using Cheaper_Effort.Models;

using Cheaper_Effort.Serivces;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

var config = new LoggingConfiguration();

try
{
    var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
    {
        EnvironmentName = Environments.Production
    });

    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

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

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseStatusCodePages();

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapRazorPages();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch(Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit
    NLog.LogManager.Shutdown();
}