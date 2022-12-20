using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Cheaper_Effort.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Principal;

namespace Cheaper_Effort.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public IFormFile Picture { get; set; }

        [BindProperty]
        public Account Account { get; set; }

        private ProjectDbContext _context;
        private IUserService _userService;

        public RegisterModel(ProjectDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public void onGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Picture");

            if (!ModelState.IsValid) return Page();


            if (_userService.CheckUserRegister(Account.Username, Account.Email))
            {
                ModelState.AddModelError("Usename", "Wrong username or email input");
                return Page();
            }

            else
                
            {
                await _userService.AddToDBasync(Account, Picture);

                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, Account.Username),
                    new Claim("Picture", Account.Picture == null ? "empty" : Convert.ToBase64String(Account.Picture))

                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
                
                return RedirectToPage("/Index");
            }

        }
    }
}
