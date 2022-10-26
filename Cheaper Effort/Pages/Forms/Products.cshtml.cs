using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static Cheaper_Effort.Pages.Forms.ProductsModel;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Microsoft.Data.SqlClient;

namespace Cheaper_Effort.Pages.Forms
{
    public class ProductsModel : PageModel
    {

        private readonly ProjectDbContext _context;
        public ProductsModel(ProjectDbContext context)
        {
            _context = context;
        }

        public async void OnGet()
        {

            Ingredients = _context.Ingredients.Select(ingredient => new Ingredient()
            {
                Id = ingredient.Id,
                IngredientName = ingredient.IngredientName
            }).ToList();

        }

        public IEnumerable<Ingredient> Ingredients { get; set; } = Enumerable.Empty<Ingredient>();

    }

}
