using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SQLitePCL;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Principal;

namespace Cheaper_Effort.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login login { get; set; }

        private ProjectDbContext _context;

        public LoginModel(ProjectDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {

        }

    public async Task<IActionResult> OnPostAsync()
        {
           if(!ModelState.IsValid) return Page();

            if (_context.User.Any(o => o.Username == login.Username && o.Password == login.Password))
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, login.Username)
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return RedirectToPage("/Index");
            }

            return Page();
    }

       
    }
}
