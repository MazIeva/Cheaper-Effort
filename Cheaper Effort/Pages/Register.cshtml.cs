﻿using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Cheaper_Effort.Serivces;
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
            if (!ModelState.IsValid) return Page();

            if (_userService.CheckUserRegister(Account.Username, Account.Email, _context))
            {
                ModelState.AddModelError("Usename", "Wrong username or email input");
                return Page();
            }

            else
                
            {
                _userService.AddToDB(Account, _context);
                ClaimsPrincipal claimsPrincipal = _userService.SetName(Account.Username, _context);
                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
                

                return RedirectToPage("/Index");
            }

        }

    }
}
