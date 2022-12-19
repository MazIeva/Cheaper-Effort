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

            

            foreach (string item in ingredientIds)
            {
                int ingredientId;
                if (!checkIfNumber(item))
                {
                    ingredientId = _context.Ingredients.FirstOrDefault(o => o.IngredientName == item).Id;
                }
                else
                {
                    ingredientId = Int32.Parse(item);
                }

                _context.Recipe_Ingredients.Add(
                    new Recipe_Ingredient
                    {
                        IngredientId = ingredientId,
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

        public bool checkIfNumber(string id)
        {
            int numericValue;
            return (int.TryParse(id, out numericValue));
        }

        public IEnumerable<Ingredient> GetNewIngredients( string[] ids)
        {
            
            int maxId = _context.Ingredients.Max(t => t.Id);
            List<Ingredient> newIngredients= new List<Ingredient>();
            foreach ( string name in ids)
            {
                if(!checkIfNumber(name))
                {
                    newIngredients.Add(new Ingredient
                    {
                        Id = maxId + 1,
                        IngredientName = name
                    }); 

                }
            }
             return newIngredients;
        }


       public async Task addNewIngredients(IEnumerable<Ingredient> ingredients)
        {
            foreach( Ingredient ingredient in ingredients)
            {
                await _context.Ingredients.AddAsync(ingredient);
            }
            _context.SaveChanges();
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

