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

namespace Cheaper_Effort.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly ProjectDbContext _context;
        [BindProperty]
        public Account Account { get; set; }

        public Discount Discount { get; set; }

        public ProfileModel(ProjectDbContext context)
        {
            _context = context;
        }
        public async void OnGet()
        {
            var username = User.Identity.Name;
            Account = _context.User.SingleOrDefault(o => o.Username.Equals(username));
        }

        public async Task OnPost()
        {
            /*Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");*/
            if(Account.UserPoints >= 200)
            {

                String guid = System.Guid.NewGuid().ToString();

                int type = (int)Discount.DiscountsType;

                if((Account.UserPoints >=400 && type == 0)
                    || (Account.UserPoints >= 1000 && type == 1)
                    || (Account.UserPoints >= 2000 && type == 2))
                {
                    Discount.Code = guid;

                    Discount.DateClaimed = DateTime.UtcNow.Date;

                }


                await _context.SaveChangesAsync();
            }
            

        }

    }
}
