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

namespace Cheaper_Effort.Pages
{
    public class LoginModel : PageModel
    {

        private readonly LoginDbContext _context;

        public LoginModel(LoginDbContext context)
        { 
            _context = context;
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid) return Page();

            if (_context.Logins.Any(o => o.Username == Login.Username && o.Password == Login.Password))
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, Login.Username)
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return RedirectToPage("/Index");
            }

            return Page();
        }

        [BindProperty]
        public Login Login { get; set; }
    }
}
