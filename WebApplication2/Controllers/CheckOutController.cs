using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe.Climate;
using System.Security.Claims;
using WebApplication2.DTO.Request;
using WebApplication2.Services;
using static WebApplication2.Models.Order;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckOutController : ControllerBase
    {



        private readonly ICartService cartService;
        private readonly IOrderService orderService;


       public CheckOutController(ICartService cartService,IOrderService orderService) {


            this.cartService = cartService;
            this.orderService = orderService;
        }


        [HttpGet("Pay")]
        public async Task<IActionResult> Pay([FromBody] PaymentRequest request )

        {

            var appUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var carts = await cartService.GetAsync(e => e.ApplicationUserId == appUser, [e=>e.Product]);

            if (carts is not null) {
                Order order = new()
                {
                    orderStatus = OrderStatus.Pending ,

                    OrderDate = DateTime.Now ,

                    TotalPrice = carts.Sum(e => e.Product.price * e.Count),
                    ApplicationUserId =  appUser

                };

                if (request.PaymentMethod == "Cash") {

                    order.paymentMethodType = PaymentMethodType.Cash;

                    await orderService.AddAsync(order);

                    return Ok();
                }

                else if (request.PaymentMethod == "Visa"){

                    order.paymentMethodType = PaymentMethodType.Visa;

                    var options = new SessionCreateOptions
                    {
                        PaymentMethodTypes = new List<string> { "card" },
                        LineItems = new List<SessionLineItemOptions>(),
                        Mode = "payment",
                        SuccessUrl = $"{Request.Scheme}://{Request.Host}/api/checkout/Success",
                        CancelUrl = $"{Request.Scheme}://{Request.Host}/checkout/cancel",
                    };

                    foreach (var item in carts)

                    {

                        options.LineItems.Add(

                           new SessionLineItemOptions
                           {
                               PriceData = new SessionLineItemPriceDataOptions

                               {
                                   Currency = "USD",
                                   ProductData = new SessionLineItemPriceDataProductDataOptions

                                   {
                                       Name = item.Product.Name,
                                       Description = item.Product.Description,
                                   },
                                   UnitAmount = (long)item.Product.price,
                               },
                               Quantity = item.Count,
                           });


                    }

                    var service = new SessionService();
                    var session = service.Create(options);
                    return Ok(new { session.Url });
                }
                else
                {
                    return BadRequest();
                }

            }




            else // cart is empty     
            {
                return NotFound();
            }
        }


        [HttpGet("")]
        public async Task<IActionResult> Success() {

            return Ok(new { message = "Done" });
                }



    }
}
