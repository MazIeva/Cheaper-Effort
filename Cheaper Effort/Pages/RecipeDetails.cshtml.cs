using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Cheaper_Effort.Serivces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Cheaper_Effort.Pages
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
        public void OnGet(Guid id)
        {
            recipeDetails = _recipeService.GetRecipeById(id);
        }
    }
}
