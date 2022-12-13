using System;
using Cheaper_Effort.Models;
using Cheaper_Effort.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Collections.Generic;

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
                Creator = recipe.Creator,
                CategoryType = recipe.CategoryType,
                Points = recipe.Points,
                Instructions = recipe.Instructions,
                Ingredients = recipe.Recipe_Ingredients.Select(n => n.Ingredient.IngredientName).ToList()
            }).ToList();
             
             
        }
        public RecipeWithIngredients GetRecipeById(Guid Id)
        {
            List<RecipeWithIngredients> recipe = new List<RecipeWithIngredients>();
            recipe = GetRecipes().ToList();

            return recipe.FirstOrDefault(o => o.Id == Id);

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

       /* public RecipeWithIngredients Update(RecipeWithIngredients RecipeNew)
        {
            var recipe = GetRecipeById(RecipeNew.Id);
            

            return recipe.FirstOrDefault(o => o.Id == Id);

        }*/

        public async Task Delete(Guid id)
        {
            var recipe = _context.Recipes.FirstOrDefault(s => s.Id == id);
            
                _context.Recipes.Remove(recipe);
            
            
                var recipeIngredients = _context.Recipe_Ingredients.FirstOrDefault(s => s.RecipeId== id);
            
                _context.Recipe_Ingredients.Remove(recipeIngredients);
            
               
            await _context.SaveChangesAsync();
        }


    }
}

