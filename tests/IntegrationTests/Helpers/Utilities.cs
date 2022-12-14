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
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(ProjectDbContext db)
        {
            db.Recipes.RemoveRange(db.Recipes);
            InitializeDbForTests(db);
        }

        public static List<Recipe> GetSeedingMessages()
        {
            Guid guid3 = new Guid("418abb9d-52f8-4977-be34-660603750e82");

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
                    Creator = "BetKas",
                    Picture = null,
                    Time = 1,
                    Difficult_steps = 2
                },

                new Recipe()
                {
                    Id = new Guid("418abb9d-52f8-4977-be34-660603750e82"),
                    Name = "Chicken",
                    Instructions = "Cook",
                    Creator = "BetKas",
                    Picture = null,
                    Time = 3,
                    Difficult_steps = 4
                }

            };
        }
    }
}
