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
       public IEnumerable<RecipeWithIngredients> SetRecipes( ProjectDbContext _context)
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
       public IEnumerable<RecipeWithIngredients> SearchRecipe( ProjectDbContext _context, string[] ingredientIds, IEnumerable<RecipeWithIngredients> RecipesWithIngredients)
        {
            List<string> products = new List<String>();
            Ingredient x = new Ingredient();

             foreach (string ingredientId in ingredientIds)
             {
                x = _context.Ingredients.SingleOrDefault(p => p.Id == Int32.Parse(ingredientId));

                products.Add(x.IngredientName);
             }


            return from recipe in RecipesWithIngredients
                   where products.All(itm => recipe.Ingredients.Contains(itm))
                   select recipe;

        }
    }
}

