using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Services.IServices;

namespace WebApplication2.Services
{
    public class CartService : Service<Cart>, ICartService
    {

        ApplicationDbContext _context;
        public CartService(ApplicationDbContext context) : base(context)
        {
            this._context = context;

        }
        


public async Task<Cart> AddToCart(string UserId, int ProductId, CancellationToken cancellationToken)

        {

            var exisitingCartItems = await _context.Carts.FirstOrDefaultAsync(e => e.ApplicationUserId == UserId && e.ProductId == ProductId);

            if (exisitingCartItems is not null)
            {

                exisitingCartItems.Count += 1;

                await _context.SaveChangesAsync(cancellationToken);
            }

            else
            {

                exisitingCartItems = new Cart
                {

                    ApplicationUserId = UserId,

                    ProductId = ProductId,

                    Count = 1
                };


                await _context.Carts.AddAsync(exisitingCartItems, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);


            }

            return exisitingCartItems;
        }

        public async Task<IEnumerable<Cart>> GetUserCartAsync(string UserId)
        {

            return await GetAsync(e => e.ApplicationUserId == UserId, includes: [c=>c.Product]);


        }
        
    } }
