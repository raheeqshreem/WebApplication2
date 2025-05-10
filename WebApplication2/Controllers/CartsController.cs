using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication2.DTO.Response;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {

        private readonly ICartService cartService;
        private readonly UserManager<ApplicationUsr> userManager;


        public CartsController(ICartService cartService,UserManager<ApplicationUsr> userManager)
        {
            this.cartService = cartService;
            this.userManager = userManager;
        }
        [HttpPost("{productId}")]

        public async Task<IActionResult> AddToCart([FromRoute]int productId,CancellationToken cancellationToken)
        {
            var appUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var result = await cartService.AddToCart(appUser, productId, cancellationToken);
            return Ok();
        }




        [HttpGet("")]

    

public async Task<IActionResult> GetUserCartAsync()

        {

            var appUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var cartItems = await cartService.GetUserCartAsync(appUser);

            var cartResponse = cartItems.Select(e => e.Product).Adapt<IEnumerable<CartResponse>>();

            var totalPrice = cartItems.Sum(e => e.Product.price * e.Count);

            return Ok(new { cartResponse, totalPrice });

        }



    }
}
