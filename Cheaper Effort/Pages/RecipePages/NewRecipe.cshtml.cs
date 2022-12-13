﻿
using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Cheaper_Effort.Serivces;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Cheaper_Effort.Pages.RecipePages
{
    public class NewRecipeModel : PageModel
    {
        [BindProperty]
        public IFormFile? Picture { get; set; }
        [BindProperty]
        public Recipe Recipe { get; set; }
        private readonly ProjectDbContext _context;
        private INewRecipeService _newRecipeService;

        [Required]
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

        public async Task<IActionResult> OnPost(string[] IngredientIds)
        {

            ModelState.Remove("Recipe.Recipe_Ingredients");
            ModelState.Remove("Picture");

            if (!ModelState.IsValid)
            {
                OnGet();
                return Page();
            }


            Recipe.Creator = User.Identity.Name;

            await _newRecipeService.addRecipeToDBAsync(Recipe, Ingredients, IngredientIds, Picture);



            return RedirectToPage("/RecipePages/Recipes");
        }


    }
}