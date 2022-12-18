using System;
using System.Security.Claims;
using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using Microsoft.AspNetCore.Authentication;
using Cheaper_Effort.Data.Migrations;


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
        public string? GetUserPicture(string username, string password)
        {
            Account acc = _context.User.First(o => o.Username == username && o.Password == password);

            return acc.Picture == null ? "" : Convert.ToBase64String(acc.Picture);
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
            if (picture != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await picture.CopyToAsync(memoryStream);
                    Account.Picture = memoryStream.ToArray();
                }
            }
        }

        /*public async Task AddPointToDBAsync(int Points, Account Account)
        {
            Account.Points = Account.Points + Points;

            _context.User.Attach(Account);
            _context.Entry(Account).Property(x => x.Points).IsModified = true;
            _context.SaveChanges();
        }*/

        public async Task SubtractPointToDBAsync(int Points, Account Account)
        {
            Account.Points = Account.Points - Points;

            _context.User.Attach(Account);
            _context.Entry(Account).Property(x => x.Points).IsModified = true;
            _context.SaveChanges();

        }

        public async Task AddDiscountToDB(string Name, Discount Discount)
        {
            Discount.Claimer = Name;

            string code = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            Discount.Code = code;

            await _context.Discounts.AddAsync(Discount);
            await _context.SaveChangesAsync();
        }

    }
}