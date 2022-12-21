using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Cheaper_Effort.Services;
using System.Linq.Expressions;

namespace Cheaper_Effort.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly ProjectDbContext _context;
        [BindProperty]
        public Account Account { get; set; }

        private IUserService _userService;

        [BindProperty]
        public Discount Discount { get; set; }

        public ProfileModel(ProjectDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public string discount5;
        public string discount10;
        public string discount15;
        public async void OnGet()
        {
            var username = User.Identity.Name;
            Account = _context.User.SingleOrDefault(o => o.Username.Equals(username));
            updateDiscounts(username);
        }

        public IActionResult OnPost()
        {
            var username = User.Identity.Name;
            Account = _context.User.SingleOrDefault(o => o.Username.Equals(username));

            var points = _userService.DiscountCheck(Account, Discount);
            if (points == null)
            {
                ModelState.AddModelError("code", "You dont have enough points");
            }
            else
            {
                    _userService.SubtractPointToDBAsync(points.Value, Account, Discount);
                    updateDiscounts(username);
            }
            

            return Page();

        }

        void updateDiscounts(string username)
        {
            discount5 = _userService.GetLastDiscount(username, Discounts.Discount5);
            discount10 = _userService.GetLastDiscount(username, Discounts.Discount10);
            discount15 = _userService.GetLastDiscount(username, Discounts.Discount15);
        }
    }
}

