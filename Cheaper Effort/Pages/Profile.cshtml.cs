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
        public ProfileModel(ProjectDbContext context)
        {
            _context = context;
        }
        public async void OnGet()
        {
            var username = User.Identity.Name;
            Account = _context.User.SingleOrDefault(o => o.Username.Equals(username));
        }
        
    }
}
