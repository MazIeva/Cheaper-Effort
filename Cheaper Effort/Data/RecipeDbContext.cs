﻿using Cheaper_Effort.Models;
using Microsoft.EntityFrameworkCore;

namespace Cheaper_Effort.Data
{
    public class RecipeDbContext : DbContext
    {
        public RecipeDbContext(DbContextOptions options) : base(options)
        { 
            
        }

        public DbSet<Recipe> Recipes { get; set; }
    }
}
