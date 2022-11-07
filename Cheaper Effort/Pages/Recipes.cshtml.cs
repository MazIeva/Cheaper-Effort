using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

            //Laikinas hardcodintas sarasas, kad rodytu receptus
            List<string> products = new List<String>()
            {

                "Pasta",
                "Butter",
                "Cheese",
                "Pepper",
                "Onion",
                "Curry",
                "Mayo",
                "Potato",
                "Egg",
                "Flour",
                "Salt"

            };


            RecipesWithIngredientsFiltered = from recipe in RecipesWithIngredients
                                             where recipe.Ingredients.All(itm => products.Contains(itm))
                                             select recipe;
        }

        public IEnumerable<RecipeWithIngredients> RecipesWithIngredients { get; set; } = Enumerable.Empty<RecipeWithIngredients>();
        public IEnumerable<RecipeWithIngredients> RecipesWithIngredientsFiltered { get; set; } = Enumerable.Empty<RecipeWithIngredients>();
    }
}