using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cheaper_Effort.Data;
using Cheaper_Effort.Data.Migrations;
using Cheaper_Effort.Models;
using Cheaper_Effort.Serivces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Cheaper_Effort.Pages.RecipePages
{
    public class EditModel : PageModel
    {
        private readonly ProjectDbContext _context;
        private IRecipeService _recipeService;
        private INewRecipeService _newRecipeService;
        private IUserService _userService;

        public SelectList IngredientsList { get; set; }

        public List<SelectListItem> EnumCategories { get; set; }

        public EditModel(ProjectDbContext context, IRecipeService recipeService, INewRecipeService newRecipeService,  IUserService userService)
        {
            _context = context;
            _recipeService = recipeService;
            _newRecipeService = newRecipeService;
            _userService = userService;
        }
        [BindProperty]
        public Recipe Recipe { get; set; }

        [BindProperty]
        public IFormFile? Picture { get; set; }
        public IActionResult OnGet(Guid Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            Recipe = _context.Recipes.Find(Id);

            var username = User.Identity.Name;

            if (!_userService.CheckIfCreator(username, Recipe.Creator))
            {
                return RedirectToPage("/RecipePages/RecipeDetails", new { id = Id });
            }



            selectedIngredients = _recipeService.GetRecipeIngredients(Recipe);
            IngredientsList = new SelectList(_recipeService.OtherIngredients(Recipe), "Id", "IngredientName");

            if (Recipe == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string[] IngredientIds, Guid Id)
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
           var newINgre =  _newRecipeService.GetNewIngredients(IngredientIds);
            await _newRecipeService.addNewIngredients(newINgre);

            _recipeService.Update(Recipe, IngredientIds, Picture);

            return RedirectToPage("/RecipePages/RecipeDetails", new { id = Id });
        }
        public IEnumerable<Ingredient> selectedIngredients { get; set; } = Enumerable.Empty<Ingredient>();

        
    }
}

