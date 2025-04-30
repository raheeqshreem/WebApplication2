using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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

        public async Task<IActionResult> AddToCart([FromRoute]int productId,[FromQuery]int count)
        {
            var appUser = userManager.GetUserId(User);
            var cart = new Cart()
            {
                ProductId = productId,
                Count = count,
                ApplicationUserId = appUser

            };
            await cartService.AddAsync(cart);
            return Ok(cart);
        }


    }
}
