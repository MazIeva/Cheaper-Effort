using Cheaper_Effort.Models;
using Microsoft.EntityFrameworkCore;

namespace Cheaper_Effort.Data
{
    public class ProjectDbContext : DbContext
    {

        public ProjectDbContext(DbContextOptions options) : base(options)
        { 
        }

        public DbSet<Account> User { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

    }
}
