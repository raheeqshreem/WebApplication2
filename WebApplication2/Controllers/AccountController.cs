using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTO.Request;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUsr> userManager;
        private readonly SignInManager<ApplicationUsr> signInManager;
       public AccountController(UserManager<ApplicationUsr> userManager,SignInManager<ApplicationUsr>signInManager) 
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] WebApplication2.DTO.Request.RegisterRequest registerRequest)
        
        {
                var applicationUser = registerRequest.Adapt<ApplicationUsr>();
           var result= await userManager.CreateAsync(applicationUser, registerRequest.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(applicationUser, false);

                return NoContent();
            }
            return BadRequest(result.Errors);
        }


        [HttpPost("login")]
        public async Task<IActionResult > Login([FromBody] WebApplication2.DTO.Request.LoginRequest loginRequest)
        {
          var applicationUser=  await userManager.FindByEmailAsync(loginRequest.Email);
            if (applicationUser !=null)
            {
                var result =  await userManager.CheckPasswordAsync(applicationUser, loginRequest.Password);
                if (result)
                {
                    await signInManager.SignInAsync(applicationUser,loginRequest.RememberMe);
                    return NoContent();
                }


            }

            return BadRequest(new { message = "invaild email or password" });


        }
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return NoContent();
        }

        [Authorize]

        [HttpPost("ChangePassword")]
        public  async Task< IActionResult >ChangePassword(ChangePasswordRequest changePasswordRequest)
        {

            var applicationUser = await userManager.GetUserAsync(User);
            if(applicationUser != null)
            {
                var  result  =await userManager.ChangePasswordAsync(applicationUser, changePasswordRequest.OldPassword, 
                    changePasswordRequest.NewPassword);
                if (result.Succeeded)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest(result.Errors);
                }


            }

        }


    }
}
