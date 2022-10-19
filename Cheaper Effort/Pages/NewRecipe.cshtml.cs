using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            await _context.Recipes.AddAsync(Recipe);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Recipes");
        }

        [BindProperty]
        public Recipe Recipe { get; set; }
        [BindProperty]
        public Recipe_Ingredient Recipe_Ingredient { get; set; }
    }
}