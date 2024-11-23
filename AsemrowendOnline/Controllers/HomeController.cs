using System.Diagnostics;
using AsemrowendOnline.Data;
using AsemrowendOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace AsemrowendOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly OnlineShopDbContext _context;

        public HomeController(OnlineShopDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var featuredProducts = _context.Products.Take(5).ToList();
            return View(featuredProducts);
            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
