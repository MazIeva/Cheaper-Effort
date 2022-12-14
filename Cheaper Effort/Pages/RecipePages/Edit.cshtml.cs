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

        public SelectList IngredientsList { get; set; }

        public List<SelectListItem> EnumCategories { get; set; }

        public EditModel(ProjectDbContext context, IRecipeService recipeService, INewRecipeService newRecipeService)
        {
            _context = context;
            _recipeService = recipeService;
            _newRecipeService = newRecipeService;
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
            List<int> ingredients = new List<int>();


            Recipe = _context.Recipes.Find(Id);
            var i = _context.Recipe_Ingredients.Where(o => o.RecipeId == Id);
            foreach (Recipe_Ingredient recipe_Ingredient in i)
            {
                ingredients.Add(recipe_Ingredient.IngredientId);
            }

            selectedIngredients = _recipeService.Ingredients(ingredients);
            IngredientsList = new SelectList(_recipeService.OtherIngredients(ingredients), "Id", "IngredientName");

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


            Recipe.Picture = GetByteArrayFromImage(Picture);
            Recipe.Points = _newRecipeService.CalculatePoints(Recipe, IngredientIds);

            var i = _context.Recipe_Ingredients.Where(o => o.RecipeId == Id);

            

            foreach (string ingredientId in IngredientIds)
            {

                foreach (Recipe_Ingredient recipe_Ingredient in i)
                {
                    if (recipe_Ingredient.IngredientId != Int32.Parse(ingredientId))
                    {
                        
                            _context.Recipe_Ingredients.Add(
                             new Recipe_Ingredient
                            {
                                 IngredientId = Int32.Parse(ingredientId),
                                 RecipeId = Id,
                            });
                        
                    }
                    
                }
            }

            _context.Entry(Recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                  throw;
               
            }

          

           
           //await _recipeService.Update(Recipe, ids);

            return RedirectToPage("/RecipePages/RecipeDetails", new { id = Id });
        }
        public IEnumerable<Ingredient> selectedIngredients { get; set; } = Enumerable.Empty<Ingredient>();

        private static byte[] GetByteArrayFromImage(IFormFile? file)
        {
            if(file == null)
            {
                return null;
            }
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                return target.ToArray();
            }
        }
    }
}
