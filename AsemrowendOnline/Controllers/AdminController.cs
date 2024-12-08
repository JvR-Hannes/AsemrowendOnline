using AsemrowendOnline.Data;
using AsemrowendOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AsemrowendOnline.Controllers
{
    public class AdminController : Controller
    {
        private readonly OnlineShopDbContext _context;

        public AdminController(OnlineShopDbContext context)
        {
            _context = context;
        }

        // View all products
        public IActionResult Index()
        {
            var products = _context.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .ToList();

            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View();
        }

        // Create Product (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            // Check ModelState
            if (ModelState.IsValid)
            {
                try
                {
                    // Add the product to the database
                    _context.Products.Add(product);
                    _context.SaveChanges(); // Save Product first to get its ID

                    // Add category association if CategoryId is provided
                    if (product.CategoryId.HasValue)
                    {
                        var productCategory = new ProductCategory
                        {
                            ProductId = product.Id, // Use the generated Product ID
                            CategoryId = product.CategoryId.Value
                        };

                        _context.ProductCategories.Add(productCategory);
                        _context.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log or display the error for debugging
                    Console.WriteLine($"Error: {ex.Message}");
                    ModelState.AddModelError("", "An error occurred while saving the product. Please try again.");
                }
            }

            // Repopulate dropdown in case of error
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View(product);
        }

        // Edit product (GET)
        public IActionResult Edit(int id)
        {
            var product = _context.Products
        .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
        .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            // Populate dropdown for categories
            ViewBag.CategoryList = new SelectList(_context.Categories.ToList(), "Id", "Name");

            // Map CategoryId if needed (for the dropdown pre-selection)
            product.CategoryId = product.ProductCategories.FirstOrDefault()?.CategoryId;

            _context.SaveChanges();

            return View(product);
        }

        // Edit Product (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _context.Products
                    .Include(p => p.ProductCategories)
                    .FirstOrDefault(p => p.Id == product.Id);

                if (existingProduct == null)
                {
                    return NotFound();
                }

                try
                {
                    // Update basic fields
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;

                    // Load ProductCategories explicitly
                    _context.Entry(existingProduct).Collection(p => p.ProductCategories).Load();

                    // Remove existing categories
                    foreach (var category in existingProduct.ProductCategories.ToList())
                    {
                        _context.ProductCategories.Remove(category);
                    }

                    // Add new category if provided
                    if (product.CategoryId.HasValue)
                    {
                        existingProduct.ProductCategories.Add(new ProductCategory
                        {
                            ProductId = product.Id,
                            CategoryId = product.CategoryId.Value
                        });
                    }

                    // Mark the product entity as modified
                    _context.Entry(existingProduct).State = EntityState.Modified;

                    // Save changes
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving changes: {ex.Message}");
                }
            }

            // Repopulate category dropdown
            ViewBag.CategoryList = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View(product);
        }

        // Delete Product (GET)
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // Delete product (POST)
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
