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
        private readonly ProjectDbContext _context;
        public UserService(ProjectDbContext context)
        {
            _context = context;
        }


        public bool CheckUserData(string username, string password)
        {
            return (_context.User.Any(o => o.Username == username && o.Password == password));
            }

      

        public bool CheckUserRegister(string username, string email)
        {
            return _context.User.Any(o => o.Username == username || o.Email == email);
            
        }

        public async Task AddToDBasync(Account Account, IFormFile picture)
        {
           AddPFP(Account, picture);
           await _context.User.AddAsync(Account);
           await _context.SaveChangesAsync();
        }

        public async void AddPFP(Account Account, IFormFile picture)
        {

            using (var memoryStream = new MemoryStream())
            {
                await picture.CopyToAsync(memoryStream);
                Account.Picture = memoryStream.ToArray();
            }
        }

    }
}

