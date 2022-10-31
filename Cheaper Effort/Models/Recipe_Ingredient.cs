namespace Cheaper_Effort.Models
{
    public class Recipe_Ingredient
    {
        public int Id { get; set; }

        public Guid RecipeId { get; set; }

        public Recipe Recipe {get; set;}

        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }
    }
}
