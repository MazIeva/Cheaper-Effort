using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Cheaper_Effort.Serivces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Cheaper_Effort.Pages.RecipePages
{
    public class RecipeDetailsModel : PageModel
    {
        private readonly ProjectDbContext _context;
        private IRecipeService _recipeService;
        public RecipeWithIngredients recipeDetails { get; set; }

        public RecipeDetailsModel(ProjectDbContext context, IRecipeService recipeService)
        {
            _context = context;
            _recipeService = recipeService;
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
    }
}
