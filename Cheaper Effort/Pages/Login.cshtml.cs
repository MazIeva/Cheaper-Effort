using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Cheaper_Effort.Serivces;
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
      
        private ProjectDbContext _context;
        private  IUserService _userService;

        public LoginModel(ProjectDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [BindProperty]
        public Login Login { get; set; }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            

           if(!ModelState.IsValid) return Page();


            if (_userService.CheckUserData(Login.Username, Login.Password))
            {
                string picture = _userService.GetUserPicture(Login.Username, Login.Password);
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, Login.Username),
                    new Claim("Picture", String.IsNullOrEmpty(picture) ? "empty" : picture)
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError("Usename", "Wrong username or password input");
                return Page();
                
            }

            
    }

       
    }
}
