using System;
using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cheaper_Effort.Serivces
{
    public interface IRecipeService
    {
         IEnumerable<RecipeWithIngredients> GetRecipes();
         IEnumerable<RecipeWithIngredients> SearchRecipe( string[] ingredientIds, IEnumerable<RecipeWithIngredients> RecipesWithIngredients);
    }
}

