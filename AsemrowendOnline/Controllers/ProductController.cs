using AsemrowendOnline.Data;
using AsemrowendOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace AsemrowendOnline.Controllers
{
    public class ProductController : Controller
    {
        private readonly OnlineShopDbContext _context;

        public ProductController(OnlineShopDbContext context) 
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Description = "Description 1", Price = 100, ImageUrl = "/images/product1.jpg"}
            };

            return View(products);
        }
    }
}
