using System;
using System.Security.Cryptography;
using Cheaper_Effort.Data;
using Cheaper_Effort.Data.Migrations;
using Cheaper_Effort.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cheaper_Effort.Serivces
{
    public class NewRecipeService : INewRecipeService
    {

        private readonly ProjectDbContext _context;
        public NewRecipeService(ProjectDbContext context)
        {
            _context = context;
        }

        public async Task addRecipeToDBAsync(Recipe Recipe, string[] ingredientIds)
        {
            Guid id = Guid.NewGuid();

            Recipe.Id = id;
            //Recipe.Creator = User.Identity.Name;

            await _context.Recipes.AddAsync(Recipe);
            await _context.SaveChangesAsync();

            foreach (string ingredientId in ingredientIds)
            {
                _context.Recipe_Ingredients.Add(
                    new Recipe_Ingredient
                    {
                        IngredientId = Int32.Parse(ingredientId),
                        RecipeId = id,
                    });
            }

            await _context.SaveChangesAsync();

        }
       
    }
}

