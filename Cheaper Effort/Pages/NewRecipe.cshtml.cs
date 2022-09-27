using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SQLitePCL;

namespace Cheaper_Effort.Pages
{
    public class NewRecipeModel : PageModel
    {
        private readonly RecipeDbContext _context;
        public NewRecipeModel(RecipeDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            await _context.Recipes.AddAsync(Recipe);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Recipes");
        }

        [BindProperty]
        public Recipe Recipe { get; set; }
    }
}
