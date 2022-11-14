using System;
using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cheaper_Effort.Serivces
{
    public interface IRecipeService
    {
        public IEnumerable<RecipeWithIngredients> SetRecipes( ProjectDbContext _context);
        public  IEnumerable<RecipeWithIngredients> SearchRecipe(ProjectDbContext _context, string[] ingredientIds, IEnumerable<RecipeWithIngredients> RecipesWithIngredients);
    }
}

