namespace Cheaper_Effort.Models
{
    public class RecipeWithIngredients
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public int Points { get; set; }
        public string Instructions { get; set; }
        public List<string> Ingredients { get; set; }
        public byte[] Picture { get; set; }
    }
}
