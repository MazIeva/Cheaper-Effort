using System;
using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cheaper_Effort.Serivces
{
    public interface IRecipeService
    {
         IEnumerable<RecipeWithIngredients> GetRecipes();
         RecipeWithIngredients GetRecipeById(Guid Id);
         IEnumerable<RecipeWithIngredients> SearchRecipe( string[] ingredientIds, IEnumerable<RecipeWithIngredients> RecipesWithIngredients);
         IEnumerable<Ingredient> GetRecipeIngredients(Recipe recipe);
         IEnumerable<Ingredient> OtherIngredients(Recipe recipe);
         Task Delete(Guid id);
         Task Update(Recipe Recipe, string[] ids, IFormFile? Picture);
        
    }
}

