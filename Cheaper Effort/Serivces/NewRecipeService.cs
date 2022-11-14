using System;
using System.Security.Cryptography;
using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cheaper_Effort.Serivces
{
    public class NewRecipeService : INewRecipeService
    {
        public async void addRecipeToDBAsync(Recipe Recipe, ProjectDbContext _context, string[] ingredientIds)
        {
            Guid id = Guid.NewGuid();

            Recipe.Id = id;

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

