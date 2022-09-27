using System.ComponentModel.DataAnnotations;

namespace Cheaper_Effort.Models
{
    public class Recipe
    {
        public uint Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = String.Empty;
        public int Points { get; set; }

        [Required]
        [StringLength(400)]
        public string Ingredients { get; set; } = String.Empty;

    }
}
