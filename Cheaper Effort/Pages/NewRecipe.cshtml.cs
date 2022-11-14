using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Cheaper_Effort.Serivces;
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
        private INewRecipeService _newRecipeService;
        public SelectList Ingredients { get; set; }
        public NewRecipeModel(ProjectDbContext context, INewRecipeService newRecipeService)
        {
            _context = context;
            _newRecipeService = newRecipeService;
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

            _newRecipeService.addRecipeToDBAsync(Recipe, _context, Ingredients, ingredientIds);


            return RedirectToPage("/Recipes");
        }

        [BindProperty]
        public Recipe Recipe { get; set; }


    }
}