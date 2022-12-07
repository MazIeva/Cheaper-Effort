using System;
using System.Drawing;
using System.Security.Cryptography;
using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using MessagePack.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient.Server;
using NLog.Web.Enums;

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
            Guid id = Guid.NewGuid();

            Recipe.Id = id;

            AddPicture(Recipe, picture);

            await _context.Recipes.AddAsync(Recipe);
            await _context.SaveChangesAsync();

            foreach (string ingredientId in ingredientIds)
            {
                _context.Recipe_Ingredients.Add(
                    new Recipe_Ingredient
                    {
                        IngredientId = Int32.Parse(ingredientId),
                        RecipeId = id,
                    });
            }

            await _context.SaveChangesAsync();

        }

        public async void AddPicture(Recipe Recipe, IFormFile picture)
        {
            using (var memoryStream = new MemoryStream())
            {
                await picture.CopyToAsync(memoryStream);
                Recipe.Picture = memoryStream.ToArray();
            }
        }
    }
}

