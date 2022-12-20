using Cheaper_Effort.Data;
using Cheaper_Effort.Data.Migrations;
using Cheaper_Effort.Models;
using Cheaper_Effort.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol;
using System.Diagnostics;

namespace Cheaper_Effort.Pages
{
    public class RecipeDetailsModel : PageModel
    {
        private readonly ProjectDbContext _context;
        private IRecipeService _recipeService;
        private IUserService _userService;
        public RecipeWithIngredients recipeDetails { get; set; }
        public Account Account { get; set; }

        public RecipeDetailsModel(ProjectDbContext context, IRecipeService recipeService,  IUserService userService)
        {
            _context = context;
            _recipeService = recipeService;
            _userService = userService;
        }
        public IActionResult OnGet(Guid id)
        {
            recipeDetails = _recipeService.GetRecipeById(id);

            if(recipeDetails != null)
            {
                return Page();
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult OnPostCollect(Guid id)
        {
            var username = User.Identity.Name;
            Account = _context.User.SingleOrDefault(o => o.Username.Equals(username));

            recipeDetails = _recipeService.GetRecipeById(id);

            _userService.AddPointToDBAsync(recipeDetails.Points, Account);

            return Page();
        }
    }
}
