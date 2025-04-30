using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal price { get; set; }
        public decimal Discount { get; set; }
        public string mainImg { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
        public bool status { get; set; }
        public Category category { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }



    }
}
