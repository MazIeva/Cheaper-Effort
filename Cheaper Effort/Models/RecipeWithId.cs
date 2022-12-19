using System;
namespace Cheaper_Effort.Models
{
    public class RecipeWithId
    {
        public RecipeWithIngredients RecipeWithIngredients { get; set; }
        public List<string> IngredientsIds { get; set; }
    }
}

