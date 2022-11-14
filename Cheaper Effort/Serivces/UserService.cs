using System;
using System.Security.Claims;
using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using Microsoft.AspNetCore.Authentication;


namespace Cheaper_Effort.Serivces
{
    public class UserService : IUserService 
    {

       public bool CheckUserData(string firstData, string secondData, ProjectDbContext _context)
        {
            if (_context.User.Any(o => o.Username == firstData && o.Password == secondData))
            {
                return true;
            }
            else return false;
        }

        public bool CheckUserRegister(string firstData, string secondData, ProjectDbContext _context)
        {
            if (_context.User.Any(o => o.Username == firstData || o.Email == secondData))
            {
                return true;
            }
            else return false;
        }

        public ClaimsPrincipal SetName(string username, ProjectDbContext _context)
        {
            var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, username)
                };
            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            return claimsPrincipal;
        }

       public void AddToDB(Account Account, ProjectDbContext _context)
        {
            _context.User.Add(Account);
            _context.SaveChanges();
        }

    }
}

