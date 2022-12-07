using System;
using Cheaper_Effort.Models;
using Cheaper_Effort.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using SQLitePCL;
using System.Drawing;
using Cheaper_Effort.Data.Migrations;

namespace Cheaper_Effort.Serivces
{
    public class RecipeService : IRecipeService
    {
        private readonly ProjectDbContext _context;

        public RecipeService(ProjectDbContext context)
        {
            _context = context;
        }

        public IEnumerable<RecipeWithIngredients> GetRecipes()
        {
            return _context.Recipes.Select(recipe => new RecipeWithIngredients()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Points = recipe.Points,
                Instructions = recipe.Instructions,
                Ingredients = recipe.Recipe_Ingredients.Select(n => n.Ingredient.IngredientName).ToList(),
                //Picture = BytesToImage(recipe.Picture)
                Picture = Convert.ToBase64String(recipe.Picture)
                //Picture = recipe.Picture
        }).ToList();
             
             
        }

       public IEnumerable<RecipeWithIngredients> SearchRecipe(string[] ingredientIds, IEnumerable<RecipeWithIngredients> RecipesWithIngredients)
       {
           
            List<string> products = new List<String>();
            Lazy<List<Ingredient>> ingredientsList = new Lazy<List<Ingredient>>(() => _context.Ingredients.ToList());
            

            foreach (string ingredientId in ingredientIds)
            {
                foreach (Ingredient i in ingredientsList.Value)
                {
                    if (i.Id == Int32.Parse(ingredientId))
                    {
                        products.Add(i.IngredientName);
                    }
                }
            }


            return from recipe in RecipesWithIngredients
                   where products.All(itm => recipe.Ingredients.Contains(itm))
                   select recipe;

        }

        public RecipeWithIngredients GetRecipeById(Guid id)
        {
            return GetRecipes().First(i => i.Id == id);
        }

        public static Image BytesToImage(byte[] picture)
        {
            using (var memoryStream = new MemoryStream())
            {
                /*await Recipe.Picture.CopyToAsync(memoryStream);
                Recipe.Picture = memoryStream.ToArray();*/

                /*memoryStream.Write(Recipe.Picture, 0, Recipe.Picture.Length);
                return Image.FromStream(memoryStream);*/
            }
            if (picture.Length != 0)
            {
                return (Image)(new ImageConverter().ConvertFrom(picture));
            }
            return null;
        }
    }
}

