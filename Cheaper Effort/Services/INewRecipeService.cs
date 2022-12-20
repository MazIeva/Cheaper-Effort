using System;
using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cheaper_Effort.Services
{
    public interface INewRecipeService
    {

        Task addRecipeToDBAsync(Recipe Recipe, SelectList Ingredients, string[] ingredientIds, IFormFile picture, string name);
        void AddPicture(Recipe Recipe, IFormFile picture);
        int CalculatePoints(Recipe recipe, string[] id);
        public IEnumerable<Ingredient> GetNewIngredients(string[] ids);
        Task addNewIngredients(IEnumerable<Ingredient> ingredients);
        bool checkIfNumber(string id);
    }
}

