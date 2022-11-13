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

            if (_userService.CheckUser(Login.Username, Login.Password, _context))
            {
                ClaimsPrincipal claimsPrincipal = _userService.SetName(Login.Username, _context);
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
