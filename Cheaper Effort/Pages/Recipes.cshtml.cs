using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Cheaper_Effort.Serivces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Net.WebSockets;

namespace Cheaper_Effort.Pages
{
    public class RecipesModel : PageModel
    {
        private readonly ProjectDbContext _context;
        private IRecipeService _recipeService;
        public SelectList Ingredients { get; set; }

        public RecipesModel(ProjectDbContext context, IRecipeService recipeService)
        {
            _context = context;
            _recipeService = recipeService;
        }
        public async void OnGet()
        {
            RecipesWithIngredients = _recipeService.SetRecipes(_context);
            Ingredients = new SelectList(_context.Ingredients, "Id", "IngredientName");

        }
        
        public IActionResult OnPostNew()
        {
           return RedirectToPage("/NewRecipe");
        }
        public IActionResult OnPostSearch(string[] ingredientIds)
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }


            if (ingredientIds.Any())
            {
                RecipesWithIngredients = _recipeService.SearchRecipe(_context, ingredientIds, RecipesWithIngredients);
                Ingredients = new SelectList(_context.Ingredients, "Id", "IngredientName");

                return Page();
            }
           else return RedirectToPage("/Recipes");

               
              

        }

        public IEnumerable<RecipeWithIngredients> RecipesWithIngredients { get; set; } = Enumerable.Empty<RecipeWithIngredients>();
        public IEnumerable<RecipeWithIngredients> RecipesWithIngredientsFiltered { get; set; } = Enumerable.Empty<RecipeWithIngredients>();
    }
}