using System.ComponentModel.DataAnnotations;

namespace Cheaper_Effort.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = String.Empty;
        public int Points { get; set; }

        [Required]
        [StringLength(2000)]
        public string Instructions { get; set; } = String.Empty;

        public List<Recipe_Ingredient> Recipe_Ingredients { get; set; }

    }
}
