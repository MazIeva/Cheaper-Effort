using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static Cheaper_Effort.Pages.Forms.IngredientModel;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Microsoft.Data.SqlClient;

namespace Cheaper_Effort.Pages.Forms
{
    public class IngredientModel : PageModel
    {
        private readonly ProjectDbContext _context;
        public SelectList Ingredients { get; set; }
        public IngredientModel(ProjectDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Ingredients = new SelectList(_context.Ingredients, "Id", "IngredientName");
        }

    }
}
