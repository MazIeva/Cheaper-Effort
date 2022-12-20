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

        public bool CheckIfCreator( string username, string Creator)
        {
            return username == Creator;
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

        public async Task DiscountCheck(Account Account, Discount Discount)
        {
            Discounts choice = Discount.DiscountsType;
            bool discount_claimed = false;

            switch (choice)
            {
                case Discounts.Discount5:
                    if (Account.Points >= 400)
                    {
                        SubtractPointToDBAsync(400, Account, Discount);
                    }
                    break;

                case Discounts.Discount10:
                    if (Account.Points >= 1000)
                    {
                        SubtractPointToDBAsync(1000, Account, Discount);
                    }
                    break;

                case Discounts.Discount15:
                    if (Account.Points >= 2000)
                    {
                        SubtractPointToDBAsync(2000, Account, Discount);
                    }
                    break;
            }

        }

        public async Task SubtractPointToDBAsync(int Points, Account Account, Discount Discount)
        {
            Account.Points = Account.Points - Points;

            AddDiscountToDB(Account.Username, Discount);

            _context.User.Attach(Account);
            _context.Entry(Account).Property(x => x.Points).IsModified = true;
            _context.SaveChanges();

        }

        public async Task AddDiscountToDB(string Name, Discount Discount)
        {
            Discount.Claimer = Name;

            Discount.Code = GenerateCodeDiscount();

            Discount.DateClaimed = DateTime.Today;

            await _context.Discounts.AddAsync(Discount);
            await _context.SaveChangesAsync();
        }
        public string GetLastDiscount(string ClaimerName, Discounts DiscountType)
        {
            var discountCodes = _context.Discounts.Where(o => o.Claimer == ClaimerName && o.DiscountsType == DiscountType);

            string answer = "none claimed!";


            if (discountCodes.FirstOrDefault() != null)
            {
                var discountCode = discountCodes.OrderByDescending(o => o.DateClaimed).First();
                answer = discountCode.Code;
            }
            
            return answer;
        }

        public string GenerateCodeDiscount()
        {
            string answer = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            answer = answer.Replace("=", "");
            answer = answer.Replace("+", "");
            answer = answer.Replace("-", "");
            answer = answer.Replace("/", "");

            return answer;
        }
    }
}