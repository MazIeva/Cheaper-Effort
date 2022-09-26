using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Cheaper_Effort.Pages
{
    public class RecipesModel : PageModel
    {
        private readonly RecipeDbContext _context;
        public RecipesModel(RecipeDbContext context)
        {
            _context = context;
        }
        public async void OnGet()
        {
            Recipes = await _context.Recipes.ToListAsync();
        }

        public IEnumerable<Recipe> Recipes { get; set; } = Enumerable.Empty<Recipe>();
    }
}
