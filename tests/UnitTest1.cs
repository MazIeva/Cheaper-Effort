using Cheaper_Effort.Services;
using Cheaper_Effort.Models;
using Cheaper_Effort.Pages;
using Cheaper_Effort.Data;
using Cheaper_Effort.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.InMemory;

using Microsoft.EntityFrameworkCore;

namespace tests;

public class UnitTest1
{

    private readonly DbContextOptions<ProjectDbContext> options = new DbContextOptionsBuilder<ProjectDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

    public Account CreateAccount(uint id, string name, string email, string password, int points)
    {
        return new Account
        {
            Id = id,
            Password = password,
            Username = name,
            ConfirmPassword = password,
            Email = email,
            FirstName = name,
            LastName = name,
            Points = points
        };
    }
    public Discount CreateDiscount(uint id, string name, string code, Discounts discounts, DateTime date)
    {
        return new Discount
        {
            Id = id,
            Claimer = name,
            Code = code,
            DiscountsType = discounts,
            DateClaimed = date
        };
    }
    public Recipe CreateRecipe(Guid id, string name, int number, int points, Category category)
    {
        return new Recipe
        {
            Id = id,
            Name = name,
            Creator = name,
            Instructions = name,
            Time = number,
            Difficult_steps = number,
            Points = points
        };
    }

    public Ingredient CreateIngredient(int id, string name)
    {
        return new Ingredient
        {
            Id = id,
            IngredientName = name
        };
    }

    public Recipe_Ingredient CreateRecipeIngredient(int id, Guid RecipeId, int IngredientId)
    {
        return new Recipe_Ingredient
        {
            Id = id,
            RecipeId = RecipeId,
            IngredientId = IngredientId
        };
    }

    [Fact]
    public void CheckUser_LoginData_Test()
    {
        Account account = CreateAccount(1, "aaa", "a@gmail.com", "Aaaaaaa1", 50);

        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();
            context.User.Add(account);
            context.SaveChanges();
            UserService _userService = new UserService(context);

            bool actual = _userService.CheckUserData(account.Username, account.Password);
            Assert.Equal(true, actual);
        }

    }
    [Fact]
    public void CheckIfCreator_Test()
    {
        Account account = CreateAccount(1, "Aaaa", "a@gmail.com", "Aaaaaaa1", 50);
        Recipe recipe = CreateRecipe(new Guid("07446f87-04c8-437c-8c9e-2de34b234c6b"), "Aaaa", 1, 40,
            Category.Lunch);

        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();
            context.User.Add(account);
            context.SaveChanges();
            UserService _userService = new UserService(context);

            bool actual = _userService.CheckIfCreator(account.Username, recipe.Creator);
            Assert.Equal(true, actual);
        }

    }

    [Fact]
    public void CheckUser_RegisterData_Test()
    {
        Account account = CreateAccount(1, "aaa", "a@gmail.com", "Aaaaaaa1", 50);
        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();
            context.User.Add(account);
            context.SaveChanges();

            UserService _userService = new UserService(context);

            bool actual = _userService.CheckUserRegister(account.Username, account.Email);
            Assert.Equal(true, actual);
        }


    }

    [Fact]
    public void GetRecipesTest()
    {
        Recipe_Ingredient recipeIngredient =
            CreateRecipeIngredient(1, new Guid("07446f87-04c8-437c-8c9e-2de34b234c6b"), 1);
        Ingredient ingredient = CreateIngredient(1, "Egg");
        Recipe recipe = CreateRecipe(new Guid("07446f87-04c8-437c-8c9e-2de34b234c6b"), "Aaaa", 1, 40,
            Category.Lunch);

        List<string> a = new List<string>();
        a.Add("Egg");


        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();
            context.Recipe_Ingredients.Add(recipeIngredient);
            context.Ingredients.Add(ingredient);
            context.Recipes.Add(recipe);
            context.SaveChanges();


            NewRecipeService newRecipeService = new NewRecipeService(context);
            RecipeService recipeService = new RecipeService(context, newRecipeService);

            var AllRecipes = recipeService.GetRecipes();
            var result = AllRecipes.First();

            Assert.Equal(recipe.Name, result.Name);
            Assert.Equal(recipe.Id, result.Id);
            Assert.Equal(recipe.Points, result.Points);
            Assert.Equal(recipe.Instructions, result.Instructions);
            Assert.Equal(recipe.CategoryType, result.CategoryType);
            Assert.Equal(recipe.Creator, result.Creator);
            Assert.Equal(ingredient.IngredientName, result.Ingredients.First());
        }
    }
    [Fact]
    public void GetRecipeByIdTest()
    {
        Recipe_Ingredient recipeIngredient =
            CreateRecipeIngredient(1, new Guid("07446f87-04c8-437c-8c9e-2de34b234c6b"), 1);
        Ingredient ingredient = CreateIngredient(1, "Egg");
        Recipe recipe = CreateRecipe(new Guid("07446f87-04c8-437c-8c9e-2de34b234c6b"), "Aaaa", 1, 40,
            Category.Lunch);

        List<string> a = new List<string>();
        a.Add("Egg");


        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();
            context.Recipe_Ingredients.Add(recipeIngredient);
            context.Ingredients.Add(ingredient);
            context.Recipes.Add(recipe);
            context.SaveChanges();

            NewRecipeService newRecipeService = new NewRecipeService(context);
            RecipeService recipeService = new RecipeService(context, newRecipeService);

            var result = recipeService.GetRecipeById(recipe.Id);

            Assert.Equal(recipe.Name, result.Name);
            Assert.Equal(recipe.Id, result.Id);
            Assert.Equal(recipe.Points, result.Points);
            Assert.Equal(recipe.Instructions, result.Instructions);
            Assert.Equal(recipe.CategoryType, result.CategoryType);
            Assert.Equal(recipe.Creator, result.Creator);
            Assert.Equal(ingredient.IngredientName, result.Ingredients.First());
        }
    }

    [Fact]
    public void GetRecipeIngredients_test()
    {
        Recipe_Ingredient recipeIngredient =
            CreateRecipeIngredient(1, new Guid("07446f87-04c8-437c-8c9e-2de34b234c6b"), 1);
        Ingredient ingredient = CreateIngredient(1, "Egg");
        Recipe recipe = CreateRecipe(new Guid("07446f87-04c8-437c-8c9e-2de34b234c6b"), "Aaaa", 1, 40,
            Category.Lunch);
        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();
            context.Recipe_Ingredients.Add(recipeIngredient);
            context.Ingredients.Add(ingredient);
            context.Recipes.Add(recipe);
            context.SaveChanges();

            NewRecipeService newRecipeService = new NewRecipeService(context);
            RecipeService recipeService = new RecipeService(context, newRecipeService);

            var result = recipeService.GetRecipeIngredients(recipe).ToList();

            Assert.Equal(ingredient.Id, result[0].Id);
            Assert.Equal(ingredient.IngredientName, result[0].IngredientName);
        }
    }
    [Fact]
    public void GetRestIngredients_test()
    {
        Recipe_Ingredient recipeIngredient =
            CreateRecipeIngredient(1, new Guid("07446f87-04c8-437c-8c9e-2de34b234c6b"), 1);
        Ingredient ingredient = CreateIngredient(1, "Egg");
        Recipe recipe = CreateRecipe(new Guid("07446f87-04c8-437c-8c9e-2de34b234c6b"), "Aaaa", 1, 40,
            Category.Lunch);
        Ingredient ingredient1 = CreateIngredient(2, "Carrot");
        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();
            context.Recipe_Ingredients.Add(recipeIngredient);
            context.Ingredients.Add(ingredient);
            context.Ingredients.Add(ingredient1);
            context.Recipes.Add(recipe);
            context.SaveChanges();

            NewRecipeService newRecipeService = new NewRecipeService(context);
            RecipeService recipeService = new RecipeService(context, newRecipeService);

            var result = recipeService.OtherIngredients(recipe).ToList();

            Assert.Equal(ingredient1.Id, result[0].Id);
            Assert.Equal(ingredient1.IngredientName, result[0].IngredientName);

        }
    }
    [Fact]
    public void SearchRecipeTest()
    {
        Recipe_Ingredient recipeIngredient =
            CreateRecipeIngredient(1, new Guid("07446f87-04c8-437c-8c9e-2de34b234c6b"), 1);
        Ingredient ingredient = CreateIngredient(1, "Egg");
        Recipe recipe = CreateRecipe(new Guid("07446f87-04c8-437c-8c9e-2de34b234c6b"), "Aaaa", 1, 40,
            Category.Lunch);

        Recipe_Ingredient recipeIngredient2 =
            CreateRecipeIngredient(2, new Guid("07446f87-04c8-437c-8c9e-2de34b234c67"), 1);
        Ingredient ingredient2 = CreateIngredient(2, "carrot");
        Recipe recipe2 = CreateRecipe(new Guid("07446f87-04c8-437c-8c9e-2de34b234c67"), "Aaaa", 2, 40,
            Category.Lunch);

        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();
            context.Recipe_Ingredients.Add(recipeIngredient);
            context.Recipe_Ingredients.Add(recipeIngredient2);
            context.Ingredients.Add(ingredient);
            context.Ingredients.Add(ingredient2);
            context.Recipes.Add(recipe);
            context.Recipes.Add(recipe2);
            context.SaveChanges();

            string[] a = { "Egg" };

            string[] b = { "1" };

            NewRecipeService newRecipeService = new NewRecipeService(context);
            RecipeService recipeService = new RecipeService(context, newRecipeService);

            var RecipeWithTest = recipeService.GetRecipes();
            var FilteredTest = recipeService.SearchRecipe(b, RecipeWithTest);
            var result = RecipeWithTest.First();

            Assert.Equal(recipe.Name, result.Name);
            Assert.Equal(recipe.Id, result.Id);
            Assert.Equal(recipe.Points, result.Points);
            Assert.Equal(recipe.Instructions, result.Instructions);
            Assert.Equal(recipe.CategoryType, result.CategoryType);
            Assert.Equal(recipe.Creator, result.Creator);
            Assert.Equal(ingredient.IngredientName, result.Ingredients.First());
        }
    }

    [Fact]
    public void AddRecipe_test()
    {
        Recipe_Ingredient recipeIngredient =
            CreateRecipeIngredient(1, new Guid("07446f87-04c8-437c-8c9e-2de34b234c6b"), 1);

        Recipe recipe = CreateRecipe(new Guid("07446f87-04c8-437c-8c9e-2de34b234c6b"), "Aaaa", 1, 40,
            Category.Lunch);

        Ingredient ingredient1 = CreateIngredient(1, "Carrot");
        string[] ids = { "1" };
        IFormFile file = null;
        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();
            context.Ingredients.Add(ingredient1); ;
            context.SaveChanges();

            NewRecipeService newRecipeService = new NewRecipeService(context);

             newRecipeService.addRecipeToDBAsync(recipe, ids, file, "aaa");
            var result = context.Recipes.ToList();

            var result2 = context.Recipe_Ingredients.ToList();

            Assert.Equal(recipe.Name, result[0].Name);
            Assert.Equal(recipe.Points, result[0].Points);
            Assert.Equal(recipe.Instructions, result[0].Instructions);
            Assert.Equal(recipe.CategoryType, result[0].CategoryType);
            Assert.Equal(recipe.Creator, result[0].Creator);
            Assert.Equal(recipe.Time, result[0].Time);
            Assert.Equal(recipe.Difficult_steps, result[0].Difficult_steps);

            Assert.Equal(recipeIngredient.IngredientId, result2[0].IngredientId);


        }

    }

    [Fact]
    public void CalculatePointsTest()
    {

        Recipe recipe = CreateRecipe(new Guid("07446f87-04c8-437c-8c9e-2de34b234c6b"), "Aaaa", 1, 60,
            Category.Lunch);
        string[] ids = { "2" };

        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();

            NewRecipeService newRecipeService = new NewRecipeService(context);
            var result = newRecipeService.CalculatePoints(recipe, ids);

            Assert.Equal(recipe.Points, result);
        }
    }
    [Fact]
    public void AddPointsTest()
    {
        Account account = CreateAccount(1, "aaa", "a@gmail.com", "Aaaaaaa1", 0);
        Recipe recipe = CreateRecipe(new Guid("07446f87-04c8-437c-8c9e-2de34b234c6b"), "Aaaa", 1, 60,
            Category.Lunch);
        string[] ids = { "2" };

        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();
            context.User.Add(account);


            NewRecipeService newRecipeService = new NewRecipeService(context);
            UserService userService = new UserService(context);
            var Points = newRecipeService.CalculatePoints(recipe, ids);
            userService.AddPointToDBAsync(Points, account);

            Assert.Equal(Points, account.Points);
        }
    }

    [Fact]
    public void CheckIfNumber_test()
    {
        string ids = "2";
        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();

            NewRecipeService newRecipeService = new NewRecipeService(context);

            var result = newRecipeService.checkIfNumber(ids);
            Assert.Equal(true, result);
        }
    }

    [Fact]
    public void GetNewIngredients_test()
    {
        string[] ids = { "Duona" };
        Ingredient ingredient = CreateIngredient(1, "Egg");
        Ingredient expected = CreateIngredient(2, "Duona");
        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();
            context.Ingredients.Add(ingredient);
            context.SaveChanges();

            NewRecipeService newRecipeService = new NewRecipeService(context);

            var ingredients = newRecipeService.GetNewIngredients(ids);
            var result = ingredients.First();
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.IngredientName, result.IngredientName);
        }
    }
    [Fact]
    public void AddNewIngredients_test()
    {
        string[] ids = { "Duona" };
        Ingredient ingredient = CreateIngredient(1, "Egg");
        Ingredient expected = CreateIngredient(2, "Duona");
        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();
            context.Ingredients.Add(ingredient);
            context.SaveChanges();

            NewRecipeService newRecipeService = new NewRecipeService(context);

            var ingredients = newRecipeService.GetNewIngredients(ids);

            newRecipeService.addNewIngredients(ingredients);
            List<Ingredient> ingredientInDb = context.Ingredients.ToList();
            Assert.Equal(expected.Id, ingredientInDb[1].Id);
            Assert.Equal(expected.IngredientName, ingredientInDb[1].IngredientName);

        }
    }
    [Fact]
    public void GetLastDiscountTest()
    {
        Account account = CreateAccount(1, "aaa", "a@gmail.com", "Aaaaaaa1", 50);

        Discount discount = CreateDiscount(1, "aaa", "aaabsjd", Discounts.Discount5, DateTime.Now);
        Discount discount2 = CreateDiscount(2, "aaa", "aaaa", Discounts.Discount5, DateTime.Now.AddHours(-5));
        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();
            context.Discounts.Add(discount);
            context.Discounts.Add(discount2);
            context.SaveChanges();

            UserService userService = new UserService(context);
            var result = userService.GetLastDiscount(account.Username, Discounts.Discount5);
            Assert.Equal(discount.Code, result);
        }
    }
    [Fact]
    public void GenerateCodeTest()
    {
        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();
            context.SaveChanges();

            UserService userService = new UserService(context);
            var result = userService.GenerateCodeDiscount();
            Assert.NotNull(result);
        }
    }
    [Fact]
    public void CheckDicsountTest_ShoukdBeNull()
    {
        Account account = CreateAccount(1, "aaa", "a@gmail.com", "Aaaaaaa1", 50);

        Discount discount = CreateDiscount(1, "aaa", "aaabsjd", Discounts.Discount5, DateTime.Now);
        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();
            context.Discounts.Add(discount);
            context.SaveChanges();

            UserService userService = new UserService(context);
            var result = userService.DiscountCheck(account, discount);

            Assert.Null(result);
        }
    }
    [Fact]
    public void CheckDicsountTest_ShouldReturnPoints()
    {
        Account account = CreateAccount(1, "aaa", "a@gmail.com", "Aaaaaaa1", 500);

        Discount discount = CreateDiscount(1, "aaa", "aaabsjd", Discounts.Discount5, DateTime.Now);
        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();

            UserService userService = new UserService(context);
            var result = userService.DiscountCheck(account, discount);

            Assert.Equal(400, result.Value);
        }
    }
    [Fact]
    public void SubstractPoints_ShouldbeEqual()
    {
        Account account = CreateAccount(1, "aaa", "a@gmail.com", "Aaaaaaa1", 500);

        Discount discount = CreateDiscount(1, "aaa", "aaabsjd", Discounts.Discount5, DateTime.Now);
        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();
            context.User.Add(account);
            context.Discounts.Add(discount);
            context.SaveChanges();

            UserService userService = new UserService(context);
            var points = userService.DiscountCheck(account, discount);
            var expected = account.Points - points.Value;

            userService.SubtractPointToDBAsync(points.Value, account, discount);


            Assert.Equal(expected, account.Points);
        }
    }
}
