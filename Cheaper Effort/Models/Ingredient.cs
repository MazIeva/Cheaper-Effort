namespace Cheaper_Effort.Models
{
    public class Ingredient
    {

        public int Id { get; set; }

        public string IngredientName { get; set; }
        
        //Navigation Properties
        public List<Recipe_Ingredient> Recipe_Ingredients { get; set; }
    }
}
