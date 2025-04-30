using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication2.DTO.Request;
using WebApplication2.Models;
using WebApplication2.Utility;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUsr> userManager;
        private readonly SignInManager<ApplicationUsr> signInManager;
        private readonly IEmailSender emailSender;
       public AccountController(UserManager<ApplicationUsr> userManager,SignInManager<ApplicationUsr>signInManager,IEmailSender emailSender) 
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] WebApplication2.DTO.Request.RegisterRequest registerRequest)
        
        {
                var applicationUser = registerRequest.Adapt<ApplicationUsr>();
           var result= await userManager.CreateAsync(applicationUser, registerRequest.Password);
            if (result.Succeeded)
            {

                await emailSender.SendEmailAsync(applicationUser.Email, "Welcome",
                    $"<h1> Hello..{ applicationUser.UserName} </h1 > <p> WebApplication2, new account <p/> ");

                await userManager.AddToRoleAsync(applicationUser, StaticData.Customer);

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

                List <Claim> claims = new();

                claims.Add(new(ClaimTypes.Name, applicationUser .UserName));

                var userRoles =await userManager.GetRolesAsync(applicationUser);
                if (userRoles.Count > 0)
                {

                    foreach (var item in userRoles)
                    {
                        claims.Add(new(ClaimTypes.Role, item));
                    }
                }
                if (result)
                {

                    SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes("b2PoI959Z00AcgyDmBU7K8lxI1LWIDV6")); 
                    SigningCredentials signingCredentials =new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                    var jwtToken = new JwtSecurityToken(

                    claims: claims,

                    expires: DateTime.Now.AddMinutes(30), 
                    signingCredentials: signingCredentials
                    );

                    string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                    return Ok(new { token });
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
        public  async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
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
            return BadRequest();

        }


    }
}
