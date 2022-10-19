using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Cheaper_Effort.Pages.Forms.ProductsModel;

namespace Cheaper_Effort.Pages.Forms
{
    public class ProductsModel : PageModel
    {
        private readonly ILogger<ProductsModel> _logger;
        public class Product
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
        }
        [BindProperty]
        public IList<SelectListItem> Products { get; set; }
        [TempData]
        public string SelectedProducts { get; set; }
        [TempData]
        public string SelectedProductsIDs { get; set; }
        public ProductsModel(ILogger<ProductsModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
            List<Product> ProductList = new List<Product>() {
                new Product { ProductID = 1, ProductName = "Pasta" },
                new Product { ProductID = 2, ProductName = "Butter" },
                new Product { ProductID = 3, ProductName = "Cheese" },
                new Product { ProductID = 4, ProductName = "Pepper" },
                new Product { ProductID = 5, ProductName = "Onion" },
                new Product { ProductID = 6, ProductName = "Curry" },
                new Product { ProductID = 7, ProductName = "Mayo" },
                new Product { ProductID = 8, ProductName = "Potato" },
                new Product { ProductID = 9, ProductName = "Egg" },
                new Product { ProductID = 10, ProductName = "Flour" },
                new Product { ProductID = 11, ProductName = "Salt"}
            };

            Products = ProductList.ToList<Product>().Select(m => new SelectListItem { Text = m.ProductName, Value = m.ProductID.ToString() }).ToList<SelectListItem>();

        }
        public IActionResult OnPost()
        {
            foreach (SelectListItem Product in Products)
            {
                if (Product.Selected)
                {
                    SelectedProducts = $"{Product.Text},{SelectedProducts}";
                    SelectedProductsIDs = $"{Product.Value},{SelectedProductsIDs}";
                }
            }
            if (SelectedProducts != null) { 
            SelectedProducts = SelectedProducts.TrimEnd(',');
            SelectedProductsIDs = SelectedProductsIDs.TrimEnd(',');
                System.IO.File.WriteAllText(@"Data\UserInput\AppFile.txt", this.SelectedProducts.ToString());
            return RedirectToPage("Products");
        }
            return RedirectToPage("Products");
        }
    }
}
