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
using Cheaper_Effort.Serivces;
using System.Linq.Expressions;

namespace Cheaper_Effort.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly ProjectDbContext _context;
        [BindProperty]
        public Account Account { get; set; }

        private IUserService _userService;

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
            /*string discount5 = */
        }

        public IActionResult OnPost()
        {
            int AccountPoints = Account.Points;
            Discounts choice = Discount.DiscountsType;
            bool added_points = false;

            switch (choice)
            {
                case (Discounts)0:
                    if (Account.Points >= 400)
                    {
                        _userService.SubtractPointToDBAsync(400, Account);
                        added_points = true;
                    }
                    break;
                
                case (Discounts)1:
                    if (Account.Points >= 1000)
                    {
                        _userService.SubtractPointToDBAsync(1000, Account);
                        added_points = true;
                    }
                    break;

                case (Discounts)2:
                    if (Account.Points >= 2000)
                    {
                        _userService.SubtractPointToDBAsync(2000, Account);
                        added_points = true;
                    }
                    break;
            }

            /*if(added_points == true)
            {
                AddDiscountToDB(Account.Username, Discount);
            }*/

            return Page();

        }
        /*string GetLastDiscount(string ClaimerName, Discounts DiscountType)
        {
            string discountCode = (from Discount in _context.Discounts
                           orderby Discount.DateClaimed ascending
                        where (Discount.Claimer == ClaimerName
                        & Discount.DiscountsType == DiscountType)
                        select Discount.Code).FirstOrDefault();
            return discountCode;
        }*/

    }
}

