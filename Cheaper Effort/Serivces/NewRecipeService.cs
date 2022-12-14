using System;
using System.Security.Cryptography;
using Cheaper_Effort.Data;
using Cheaper_Effort.Data.Migrations;
using Cheaper_Effort.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        public async Task addRecipeToDBAsync(Recipe Recipe, SelectList Ingredients, string[] ingredientIds, IFormFile picture)

        {
            int points = 0;
            
            Guid id = Guid.NewGuid();

            Recipe.Id = id;
            //Recipe.Creator = User.Identity.Name;

            AddPicture(Recipe, picture);

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

            Recipe.Points = CalculatePoints(Recipe, ingredientIds);

            await _context.SaveChangesAsync();

        }
        public async void AddPicture(Recipe Recipe, IFormFile picture)
        {
            if(picture != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await picture.CopyToAsync(memoryStream);
                    Recipe.Picture = memoryStream.ToArray();
                }
            }
        }

        public int CalculatePoints(Recipe recipe, string[] id)
        {
            int points = 0;
            points = points + ((int)(Math.Round(recipe.Time)) * 20) + (recipe.Difficult_steps * 30);

            foreach (string ingredientId in id)
            {
                points = points + 10;
            }


                return points;
        }
    }
}

