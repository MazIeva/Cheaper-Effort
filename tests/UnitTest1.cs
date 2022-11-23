using Cheaper_Effort.Serivces;
using Cheaper_Effort.Models;
using Cheaper_Effort.Pages;
using Cheaper_Effort.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.InMemory;

using Microsoft.EntityFrameworkCore;

namespace tests;

public class UnitTest1
{

    private readonly DbContextOptions<ProjectDbContext> options = new DbContextOptionsBuilder<ProjectDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

    [Fact]
    public void CheckUser_LoginData_Test()
    {
        
        
        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();
            context.User.Add(new Account
            {
                Id = 1,
                Password = "Aaaaaaaa1",
                Username = "aa",
                ConfirmPassword = "Aaaaaaaa1",
                Email = "a@gmail.com",
                FirstName = "a",
                LastName = "a"
            });
            context.SaveChanges();
            UserService a = new UserService(context);

            bool user = a.CheckUserData("aa", "Aaaaaaaa1");
            Assert.Equal(user, true);
        }

    }

    [Fact]
    public void CheckUser_RegisterData_Test()
    {

        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();
            context.User.Add(new Account
            {
                Id = 1,
                Password = "Aaaaaaaa1",
                Username = "aa",
                ConfirmPassword = "Aaaaaaaa1",
                Email = "a@gmail.com",
                FirstName = "a",
                LastName = "a"
            });
            context.SaveChanges();
            UserService a = new UserService(context);

            bool user = a.CheckUserRegister("aa", "Aaaaaaaa1");
            Assert.Equal(user, true);
        }
        

    }

    [Fact]
    public void GetRecipesTest()
    {
        Recipe_Ingredient RI = new Recipe_Ingredient();
        List<Recipe_Ingredient> RIS = new List<Recipe_Ingredient>();
        Ingredient I = new Ingredient();
        Recipe R = new Recipe();

        IEnumerable<RecipeWithIngredients> RecipeWithTest = new List<RecipeWithIngredients>();
        List<string> a = new List<string>();
        a.Add("Egg");
       
        RI.Id = 1;
        RI.RecipeId = new Guid("07446f87-04c8-437c-8c9e-2de34b234c6b");
        RI.Recipe = R;
        RI.IngredientId = 2;
        RI.Ingredient = I;
        RIS.Add(RI);

        I.Id = 2;
        I.IngredientName = "Egg";
        I.Recipe_Ingredients = RIS;

        R.Id = new Guid("07446f87-04c8-437c-8c9e-2de34b234c6b");
        R.Name = "A";
        R.Points = 1;
        R.Instructions = "a";
        R.Recipe_Ingredients = RIS;
        R.CategoryType = Category.Lunch;

        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();
            context.Recipe_Ingredients.Add(RI);
            context.Ingredients.Add(I);
            context.Recipes.Add(R);
            context.SaveChanges();

            RecipeService r = new RecipeService(context);

            RecipeWithTest = r.GetRecipes();
            RecipeWithIngredients recipeWithIngredint = RecipeWithTest.First();
            
            Assert.Equal(recipeWithIngredint.Name, "A");
            Assert.Equal(recipeWithIngredint.Id, new Guid("07446f87-04c8-437c-8c9e-2de34b234c6b"));
            Assert.Equal(recipeWithIngredint.Points, 1);
            Assert.Equal(recipeWithIngredint.Instructions, "a");
            Assert.Equal(recipeWithIngredint.Ingredients, a);
        }
    }
    [Fact]
    public void SearchRecipeTest()
    {
        Recipe_Ingredient RI = new Recipe_Ingredient();
        List<Recipe_Ingredient> RIS = new List<Recipe_Ingredient>();
        Ingredient I = new Ingredient();
        Recipe R = new Recipe();

        Recipe_Ingredient RI1 = new Recipe_Ingredient();
        List<Recipe_Ingredient> RIS1 = new List<Recipe_Ingredient>();
        Ingredient I1 = new Ingredient();
        Recipe R1 = new Recipe();
        IEnumerable<RecipeWithIngredients> FilteredTest = new List<RecipeWithIngredients>();
        IEnumerable<RecipeWithIngredients> RecipeWithTest = new List<RecipeWithIngredients>();
        

        RI.Id = 1;
        RI.RecipeId = new Guid("07446f87-04c8-437c-8c9e-2de34b234c61");
        RI.Recipe = R;
        RI.IngredientId = 2;
        RI.Ingredient = I;
        RIS.Add(RI);

        I.Id = 2;
        I.IngredientName = "Egg";
        I.Recipe_Ingredients = RIS;

        R.Id = new Guid("07446f87-04c8-437c-8c9e-2de34b234c61");
        R.Name = "A";
        R.Points = 1;
        R.Instructions = "a";
        R.Recipe_Ingredients = RIS;
        R.CategoryType = Category.Lunch;


        RI1.Id = 2;
        RI1.RecipeId = new Guid("07446f87-04c8-437c-8c9e-2de34b234c62");
        RI1.Recipe = R;
        RI1.IngredientId = 3;
        RI1.Ingredient = I;
        RIS1.Add(RI);

        I1.Id = 3;
        I1.IngredientName = "Carrot";
        I1.Recipe_Ingredients = RIS1;

        R1.Id = new Guid("07446f87-04c8-437c-8c9e-2de34b234c62");
        R1.Name = "b";
        R1.Points = 2;
        R1.Instructions = "a";
        R1.Recipe_Ingredients = RIS1;
        R1.CategoryType = Category.Lunch;

        using (var context = new ProjectDbContext(options))
        {
            context.Database.EnsureCreated();
            context.Recipe_Ingredients.Add(RI);
            context.Recipe_Ingredients.Add(RI1);
            context.Ingredients.Add(I);
            context.Ingredients.Add(I1);
            context.Recipes.Add(R);
            context.Recipes.Add(R1);
            context.SaveChanges();

            string[] a = { "Egg" };
            string[] b = { "2" };

            RecipeService r = new RecipeService(context);
            RecipeWithTest = r.GetRecipes();
            FilteredTest = r.SearchRecipe( b, RecipeWithTest);
            RecipeWithIngredients recipeWithIngredint = RecipeWithTest.First();

            Assert.Equal(recipeWithIngredint.Name, "A");
            Assert.Equal(recipeWithIngredint.Id, new Guid("07446f87-04c8-437c-8c9e-2de34b234c61"));
            Assert.Equal(recipeWithIngredint.Points, 1);
            Assert.Equal(recipeWithIngredint.Instructions, "a");
            Assert.Equal(recipeWithIngredint.Ingredients, a);
        }
    }
}
