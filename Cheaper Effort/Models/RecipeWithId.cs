using System;
namespace Cheaper_Effort.Models
{
    public class RecipeWithId
    {
        public Recipe Recipe { get; set; }
        public List<string> IngredientsIds { get; set; }
    }
}

