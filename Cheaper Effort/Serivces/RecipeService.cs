using System;
using Cheaper_Effort.Models;
using Cheaper_Effort.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

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
                Ingredients = recipe.Recipe_Ingredients.Select(n => n.Ingredient.IngredientName).ToList()
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

        public IEnumerable<RecipeWithIngredients> SearchRecipe(ProjectDbContext _context, string[] ingredientIds, IEnumerable<RecipeWithIngredients> RecipesWithIngredients)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RecipeWithIngredients> SetRecipes(ProjectDbContext _context)
        {
            throw new NotImplementedException();
        }
    }
}

