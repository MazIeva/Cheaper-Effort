using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Cheaper_Effort.Serivces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cheaper_Effort.Pages.RecipePages
{
    public class EditModel : PageModel
    {
        private readonly ProjectDbContext _context;
        private IRecipeService _recipeService;
        
        public RecipeWithIngredients Recipe { get; set; }

        public SelectList Ingredients { get; set; }
        

    public EditModel(ProjectDbContext context, IRecipeService recipeService)
        {
            _context = context;
            _recipeService = recipeService;
        }

        public IActionResult OnGet(Guid Id)
        {
            Ingredients = new SelectList(_context.Ingredients, "Id", "IngredientName");
           
            
            if (Id == null)
            {
                return NotFound();
            }
            Recipe = _recipeService.GetRecipeById(Id);

            if (Recipe == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
