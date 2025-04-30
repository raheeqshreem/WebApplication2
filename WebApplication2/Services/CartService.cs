using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Services.IServices;

namespace WebApplication2.Services
{
    public class CartService : Service<Cart> , ICartService
    {

        ApplicationDbContext _context;
        public CartService(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
