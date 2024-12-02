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
            var products = _context.Products.ToList();

            return View(products);
        }
    }
}
