using WebApplication2.Models;

namespace WebApplication2.DTO.Request
{
    public class ProductRequest
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal price { get; set; }
        public decimal Discount { get; set; }
        public IFormFile mainImg { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
        public bool status { get; set; }

        public int CategoryId { get; set; }



    }
}
