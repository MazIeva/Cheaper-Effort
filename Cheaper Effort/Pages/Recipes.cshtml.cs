using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.WebSockets;

namespace Cheaper_Effort.Pages
{
    public class RecipesModel : PageModel
    {
        private readonly ProjectDbContext _context;
        public RecipesModel(ProjectDbContext context)
        {
            _context = context;
        }
        public async void OnGet()
        {

            RecipesWithIngredients = _context.Recipes.Select(recipe => new RecipeWithIngredients()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Points = recipe.Points,
                Instructions = recipe.Instructions,
                Ingredients = recipe.Recipe_Ingredients.Select(n => n.Ingredient.IngredientName).ToList()
            }).ToList();
        }

        public IEnumerable<RecipeWithIngredients> RecipesWithIngredients { get; set; } = Enumerable.Empty<RecipeWithIngredients>();
    }
}
