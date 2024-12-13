using System.ComponentModel.DataAnnotations;

namespace AsemrowendOnline.Models
{
    public class Product
    {
        public int Id {  get; set; }
        [Required(ErrorMessage = "Product name is required.")]
        public string Name { get; set; }
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Url(ErrorMessage = "Please enter a valid URL.")]
        [Required(ErrorMessage = "Image URL is required")]
        public string ImageUrl { get; set; }
        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100")]
        public decimal? Discount { get; set; }
        public int? CategoryId { get; set; }

        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }
}
