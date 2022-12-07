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
using Microsoft.EntityFrameworkCore;

namespace Cheaper_Effort.Pages.RecipePages
{
    public class DeleteModel : PageModel
    {
        private readonly ProjectDbContext _context;
        private IRecipeService _recipeService;
        public RecipeWithIngredients Recipe { get; set; }


        public DeleteModel(ProjectDbContext context, IRecipeService recipeService)
        {
            _context = context;
            _recipeService = recipeService;
        }


        /*public  IActionResult OnGet(RecipeWithIngredients recipeWithIngredients)
        {
            if (recipeWithIngredients == null)
            {
                return NotFound();
            }
            Recipe = recipeWithIngredients;
            
            return Page();
        }*/
        public IActionResult OnGet(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Recipe = _recipeService.GetRecipesById(id);
            if (Recipe == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(Guid id)
        {

            _recipeService.Delete(id);

             return RedirectToPage("/RecipePages/Recipes");

        }

    }
}
