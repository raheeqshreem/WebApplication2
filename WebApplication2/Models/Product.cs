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
        public int CategoryId { get; set; }

        public Product product { get; set; }
        public int ProductId { get; set; }


    }
}
