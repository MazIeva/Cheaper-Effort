using System;
using System.Security.Cryptography;
using Cheaper_Effort.Data;
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

        public async Task addRecipeToDBAsync(Recipe Recipe, SelectList Ingredients, string[] ingredientIds)
        {
            int points = 0;
            
            Guid id = Guid.NewGuid();

            Recipe.Id = id;

            await _context.Recipes.AddAsync(Recipe);
            await _context.SaveChangesAsync();

            points = points + ((int)(Math.Round(Recipe.Time)) * 20) + (Recipe.Difficult_steps * 30);

            foreach (string ingredientId in ingredientIds)
            {
                points = points + 10;

                _context.Recipe_Ingredients.Add(
                    new Recipe_Ingredient
                    {
                        IngredientId = Int32.Parse(ingredientId),
                        RecipeId = id,
                    });
            }

            Recipe.Points = points;

            await _context.SaveChangesAsync();

        }
    }
}

