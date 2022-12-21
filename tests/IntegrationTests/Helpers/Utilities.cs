using System.Collections.Generic;
using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace tests.IntegrationTests.Helpers
{
    public static class Utilities
    {
        public static void InitializeDbForTests(ProjectDbContext db)
        {
            db.Recipes.AddRange(GetSeedingMessages());
            db.Ingredients.AddRange(GetIngredientsSeedingMsg());
            db.Recipe_Ingredients.AddRange(GetRecipeIngredientSeedingMsg());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(ProjectDbContext db)
        {
            db.Recipes.RemoveRange(db.Recipes);
            db.Ingredients.RemoveRange(db.Ingredients);
            db.Recipe_Ingredients.RemoveRange(db.Recipe_Ingredients);           
            InitializeDbForTests(db);
        }
        public static List<Recipe> GetSeedingMessages()
        {
            return new List<Recipe>()
            {
                new Recipe()
                {
                    Id = new Guid("3a577e39-5758-4de5-b3a1-3000a9a6db1f"),
                    Name = "Pizza",
                    Instructions = "Cook",
                    Creator = "BetKas",
                    Picture = null,
                    Time = 2,
                    Difficult_steps = 5
                },

                new Recipe()
                {
                    Id = new Guid("d94c55b9-c240-478e-b853-62061ed96174"),
                    Name = "Salad",
                    Instructions = "Mix",
                    Creator = "Kitas",
                    Picture = null,
                    Time = 1,
                    Difficult_steps = 2
                },

                new Recipe()
                {
                    Id = new Guid("418abb9d-52f8-4977-be34-660603750e82"),
                    Name = "Chicken",
                    Instructions = "Cook",
                    Creator = "Trecias",
                    Picture = null,
                    Time = 3,
                    Difficult_steps = 4
                }

            };
        }
        private static List<Ingredient> GetIngredientsSeedingMsg()
        {
            return new List<Ingredient>()
            {
                new Ingredient()
                {
                    Id = 1,

                    IngredientName = "Flour"
                },

                new Ingredient()
                {
                    Id = 2,

                    IngredientName = "Chicken"
                },

                new Ingredient()
                {
                    Id = 3,

                    IngredientName = "Bread"
                },

                new Ingredient()
                {
                    Id = 4,

                    IngredientName = "Salt"
                },

                new Ingredient()
                {
                    Id = 5,

                    IngredientName = "Banana"
                }

            };
        }
        private static List<Recipe_Ingredient> GetRecipeIngredientSeedingMsg()
        {
            return new List<Recipe_Ingredient>()
            {
                new Recipe_Ingredient()
                {
                    Id = 1,
                    RecipeId = new Guid("3a577e39-5758-4de5-b3a1-3000a9a6db1f"),
                    IngredientId = 1
                },

                new Recipe_Ingredient()
                {
                    Id = 2,
                    RecipeId = new Guid("3a577e39-5758-4de5-b3a1-3000a9a6db1f"),
                    IngredientId = 2
                },

                new Recipe_Ingredient()
                {
                    Id = 3,
                    RecipeId = new Guid("3a577e39-5758-4de5-b3a1-3000a9a6db1f"),
                    IngredientId = 3
                },

                new Recipe_Ingredient()
                {
                    Id = 4,
                    RecipeId = new Guid("d94c55b9-c240-478e-b853-62061ed96174"),
                    IngredientId = 1
                },

                new Recipe_Ingredient()
                {
                    Id = 5,
                    RecipeId = new Guid("d94c55b9-c240-478e-b853-62061ed96174"),
                    IngredientId = 5
                },

                new Recipe_Ingredient()
                {
                    Id = 6,
                    RecipeId = new Guid("418abb9d-52f8-4977-be34-660603750e82"),
                    IngredientId = 3
                }
            };
        }
    }
}
