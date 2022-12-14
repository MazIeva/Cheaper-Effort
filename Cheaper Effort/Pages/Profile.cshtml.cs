using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cheaper_Effort.Data;
using Cheaper_Effort.Models;

namespace Cheaper_Effort.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly ProjectDbContext _context;
        [BindProperty]
        public Account Account { get; set; }
        public Recipe Recipe { get; set; }

        [BindProperty]
        public int Number { get; set; }
        public ProfileModel(ProjectDbContext context)
        {
            _context = context;
        }
        public async void OnGet()
        {
            var username = User.Identity.Name;
            Account = _context.User.SingleOrDefault(o => o.Username.Equals(username));
        }

        public async Task OnPostDiscount()
        {
            /*Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");*/

            if (Number == 1)
            {
                Account.Discount5 = "hello";
            }
            else if (Number == 2)
            {
                Account.Discount10 = "hi";
            }
            else if (Number == 3)
            {
                Account.Discount15 = "sup";
            }

            _context.Entry(Account).State = Microsoft.EntityFrameworkCore.EntityState.Modified; ;
            await _context.SaveChanges();
        }

    }
}
