using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsemrowendOnline.Models
{
    public class ProductCategory
    {
        [Key, Column(Order = 0)]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Key, Column(Order = 1)]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
