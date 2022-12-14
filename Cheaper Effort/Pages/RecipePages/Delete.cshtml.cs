using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Cheaper_Effort.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Cheaper_Effort.Pages.RecipePages
{
    public class DeleteModel : PageModel
    {
        private readonly ProjectDbContext _context;
        private IRecipeService _recipeService;
        public RecipeWithIngredients Recipe { get; set; }
        private IUserService _userService;


        public DeleteModel(ProjectDbContext context, IRecipeService recipeService, INewRecipeService newRecipeService, IUserService userService)
        {
            _context = context;
            _recipeService = recipeService;
            _userService = userService;
        }

        public IActionResult OnGet(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Recipe = _recipeService.GetRecipeById(id);

            var username = User.Identity.Name;

            if (!_userService.CheckIfCreator(username, Recipe.Creator))
            {
                return RedirectToPage("/RecipePages/RecipeDetails", new { id = id });
            }

            if (Recipe == null)
            {
                return NotFound();
            }
            return Page();
        }

    }
}
