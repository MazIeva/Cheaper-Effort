using Cheaper_Effort.Data;
using Cheaper_Effort.Models;

using Cheaper_Effort.Serivces;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

public partial class Program { }
