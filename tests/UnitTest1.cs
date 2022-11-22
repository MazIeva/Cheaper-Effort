using Cheaper_Effort.Serivces;
using Cheaper_Effort.Models;
using Cheaper_Effort.Pages;
using Cheaper_Effort.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.InMemory;

using Microsoft.EntityFrameworkCore;

namespace tests;

public class UnitTest1
{

    private readonly DbContextOptions<ProjectDbContext> options = new DbContextOptionsBuilder<ProjectDbContext>()
                .UseInMemoryDatabase(databaseName: "ProjectDbContext")
                .Options;

    [Fact]
    public void CheckUser_LoginData_Test()
    {
        UserService a = new UserService();
        
        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();
        }
        using (var context = new ProjectDbContext(options))
        {
            context.User.Add(new Account
            {
                Id = 1,
                Password = "Aaaaaaaa1",
                Username = "aa",
                ConfirmPassword = "Aaaaaaaa1",
                Email = "a@gmail.com",
                FirstName = "a",
                LastName = "a"
            });
            context.SaveChanges();
            
        }

       using (var context = new ProjectDbContext(options))
        {
            bool user =  a.CheckUserData("aa", "Aaaaaaaa1", context);
            Assert.Equal(user, true);
        }

    }

    [Fact]
    public void CheckUser_RegisterData_Test()
    {
        UserService a = new UserService();

        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();
        }
        using (var context = new ProjectDbContext(options))
        {
            context.User.Add(new Account
            {
                Id = 1,
                Password = "Aaaaaaaa1",
                Username = "aa",
                ConfirmPassword = "Aaaaaaaa1",
                Email = "a@gmail.com",
                FirstName = "a",
                LastName = "a"
            });
            context.SaveChanges();

        }

        using (var context = new ProjectDbContext(options))
        {
            bool user = a.CheckUserRegister("aa", "a@gmail.com", context);
            Assert.Equal(user, true);
        }

    }
}
