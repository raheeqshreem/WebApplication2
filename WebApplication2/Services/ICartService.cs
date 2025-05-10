using WebApplication2.Models;
using WebApplication2.Services.IServices;

namespace WebApplication2.Services
{
    public interface ICartService:IService<Cart>
    {
         Task<Cart> AddToCart(string UserId, int ProductId, CancellationToken cancellationToken);
        Task<IEnumerable<Cart>> GetUserCartAsync(string UserId);

    }
}
