using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Cheaper_Effort.Pages
{
    public class NewRecipeModel : PageModel
    {
        private readonly ProjectDbContext _context;
        public SelectList Ingredients { get; set; }
        public NewRecipeModel(ProjectDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Ingredients = new SelectList(_context.Ingredients, "Id", "IngredientName");
        }

        public async Task<IActionResult> OnPost(string[] ingredientIds)
        {
            ModelState.Remove("Recipe.Recipe_Ingredients");
            if (!ModelState.IsValid)
            {
                OnGet();
                return Page();
            }

            await _context.Recipes.AddAsync(Recipe);
            await _context.SaveChangesAsync();

            Ingredients = new SelectList(_context.Ingredients, "Id", "IngredientName");

            int maximum = (from recipe in _context.Recipes
                           orderby recipe.Id descending
                           select recipe.Id
                              ).FirstOrDefault();

            foreach (string ingredientId in ingredientIds)
            {
                SelectListItem selectedItem = Ingredients.ToList().Find(p => p.Value == ingredientId);

                _context.Recipe_Ingredients.Add(
                    new Recipe_Ingredient
                    {
                        IngredientId = Int32.Parse(selectedItem.Value),
                        RecipeId = maximum,
                    }) ;
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("/Recipes");
        }

        [BindProperty]
        public Recipe Recipe { get; set; }


    }
}