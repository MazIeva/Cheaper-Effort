using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Cheaper_Effort.Serivces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.WebSockets;

namespace Cheaper_Effort.Pages.RecipePages
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
                RecipesWithIngredients = _recipeService.GetRecipes(); 
                Ingredients = new SelectList(_context.Ingredients, "Id", "IngredientName"); // Publish the return value
           
        }
        
        public IActionResult OnPostNew()
        {
           return RedirectToPage("/RecipePages/NewRecipe");
        }
        public IActionResult OnPostSearch(string[] ingredientIds)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            if (ingredientIds.Any())
            {
                RecipesWithIngredients = _recipeService.SearchRecipe( ingredientIds, _recipeService.GetRecipes());
                Ingredients = new SelectList(_context.Ingredients, "Id", "IngredientName");

                return Page();
            }
           else return RedirectToPage("/RecipePages/Recipes");

        }

       

        public IEnumerable<RecipeWithIngredients> RecipesWithIngredients { get; set; } = Enumerable.Empty<RecipeWithIngredients>();
        
    }
}