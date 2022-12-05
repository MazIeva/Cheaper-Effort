using NLog.Web.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cheaper_Effort.Models
{
    public class Recipe
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }  = String.Empty;
        public int Points { get; set; }

        [Required]
        [StringLength(2000)]
        public string Instructions { get; set; } = String.Empty;
        public List<Recipe_Ingredient> Recipe_Ingredients { get; set; }

        [Required]
        [EnumDataType(typeof(Category))]
        public Category CategoryType { get; set; }

        public byte[] Picture { get; set; }

    }
    public enum Category
    {
        Breakfast,
        Lunch,
        Diner,
        Snacks,
        Dessert
    }
}
