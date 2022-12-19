using System;
using Cheaper_Effort.Models;
using Cheaper_Effort.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Collections.Generic;
using Cheaper_Effort.Data.Migrations;
using System.Collections;

namespace Cheaper_Effort.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly ProjectDbContext _context;
        private readonly INewRecipeService _newRecipeService;

        public RecipeService(ProjectDbContext context, INewRecipeService newRecipeService)
        {
            _context = context;
            _newRecipeService = newRecipeService;
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
                Ingredients = recipe.Recipe_Ingredients.Select(n => n.Ingredient.IngredientName).ToList(),
                Picture = recipe.Picture == null ? (String?) null : Convert.ToBase64String(recipe.Picture)
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
        public IEnumerable<Ingredient> GetRecipeIngredients(Recipe recipe)
        {
            
            List<Ingredient> ingredients = new List<Ingredient>();

            ingredients = _context.Recipe_Ingredients.Where(o => o.RecipeId == recipe.Id).Select(n => n.Ingredient).ToList();

            

            return ingredients;
        }

       

        public IEnumerable<Ingredient> OtherIngredients(Recipe recipe)
        {
            var ingredients = _context.Recipe_Ingredients.Where(o => o.RecipeId == recipe.Id);
            Lazy<List<Ingredient>> ingredientsList = new Lazy<List<Ingredient>>(() => _context.Ingredients.ToList());
            return from ingredient in ingredientsList.Value
                   where ingredients.All(id => id.IngredientId != ingredient.Id)
                   select ingredient;
        }

        public async Task Update(Recipe Recipe, string[] ids, IFormFile? Picture)
        {
            Recipe.Picture = GetByteArrayFromImage(Picture);
            Recipe.Points = _newRecipeService.CalculatePoints(Recipe, ids);

            List<Recipe_Ingredient> oldIngredients = _context.Recipe_Ingredients.Where(o => o.RecipeId == Recipe.Id).ToList();


            var selectedIngredient =
                         from ingredientId in ids
                         where !oldIngredients.Select(id => id.IngredientId.ToString()).Contains(ingredientId) || !oldIngredients.Select(id => id.Ingredient.IngredientName).Contains(ingredientId)
                         select ingredientId;

            var ingridientsToDelete = from oldIngredient in oldIngredients
                                      where !ids.Contains(oldIngredient.IngredientId.ToString()) || !ids.Contains(oldIngredient.Ingredient.IngredientName)
                                      select oldIngredient;

            
            foreach (var item in selectedIngredient)
            {   int id;
                if (!_newRecipeService.checkIfNumber(item))
                {
                    id = _context.Ingredients.FirstOrDefault(o => o.IngredientName == item).Id;
                }
                else
                {
                    id = Int32.Parse(item);
                }
                _context.Recipe_Ingredients.Add(
                          new Recipe_Ingredient
                          {
                              IngredientId = id,
                              RecipeId = Recipe.Id,
                          });
            }
            
            foreach(var item in ingridientsToDelete)
            {
                _context.Recipe_Ingredients.Remove(item);
            }

            _context.Entry(Recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }

        }



        private static byte[] GetByteArrayFromImage(IFormFile? file)
        {
            if (file == null)
            {
                return null;
            }
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                return target.ToArray();
            }
        }



            public async Task Delete(Guid id)
        {
            var recipe = _context.Recipes.FirstOrDefault(s => s.Id == id);
            
                _context.Recipes.Remove(recipe);
            
            
                var recipeIngredients = _context.Recipe_Ingredients.FirstOrDefault(s => s.RecipeId == id);
            
                _context.Recipe_Ingredients.Remove(recipeIngredients);
            
               
            await _context.SaveChangesAsync();
        }


    }
}

