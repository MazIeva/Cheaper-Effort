using Cheaper_Effort.Models;
using Microsoft.EntityFrameworkCore;

namespace Cheaper_Effort.Data
{
    public class ProjectDbContext : DbContext
    {

        public ProjectDbContext(DbContextOptions options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Recipe_Ingredient>()
                .HasOne(r => r.Recipe)
                .WithMany(ri => ri.Recipe_Ingredients)
                .HasForeignKey(rid => rid.RecipeId);

            modelBuilder.Entity<Recipe_Ingredient>()
                .HasOne(r => r.Ingredient)
                .WithMany(ri => ri.Recipe_Ingredients)
                .HasForeignKey(rid => rid.IngredientId);

        }
        
        public DbSet<Account> User { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe_Ingredient> Recipe_Ingredients { get; set; }
        public DbSet<Discount> Discounts { get; set; }

    }
}
