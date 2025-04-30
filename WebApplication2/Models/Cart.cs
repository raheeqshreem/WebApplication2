namespace WebApplication2.Models
{
    public class Cart
    {

        public int ProductId { get; set; }
        public string ApplicationUserId { get; set; }

        public Product Product { get; set; }
        public ApplicationUsr applicationUsr { get; set; }
        public int Count { get; set; }







    }
}
