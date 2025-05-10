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
using System.Net.Sockets;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using WebApplication2.DTO.Request;
using WebApplication2.Models;
using WebApplication2.Services;
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
        private readonly IPasswordRestCodeService passwordRestCodeService;
        public AccountController(UserManager<ApplicationUsr> userManager, SignInManager<ApplicationUsr> signInManager, IEmailSender emailSender, IPasswordRestCodeService passwordRestCodeService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.passwordRestCodeService = passwordRestCodeService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] WebApplication2.DTO.Request.RegisterRequest registerRequest)

        {
            var applicationUser = registerRequest.Adapt<ApplicationUsr>();
            var result = await userManager.CreateAsync(applicationUser, registerRequest.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(applicationUser, StaticData.Customer);

                // await emailSender.SendEmailAsync(applicationUser.Email, "Welcome",
                //  $"<h1> Hello..{ applicationUser.UserName} </h1 > <p> WebApplication2, new account <p/> ");
                var token = await userManager.GenerateEmailConfirmationTokenAsync(applicationUser);

                var emailConfirmUrl = Url.Action(nameof(ConfirmEmail), "Account", new { token, userId = applicationUser.Id },


                  protocol: Request.Scheme,//http or https host:Request.Host.Value
                  host: Request.Host.Value

                  );

                await emailSender.SendEmailAsync(applicationUser.Email, "Confirm Email",
                    $"<h1> Hello.. {applicationUser.UserName} </h1> <p> t-tshop, new account <p/> * " +
                    $" < a href = '{emailConfirmUrl}' > click here </ a > ");


                return NoContent();
            }
            return BadRequest(result.Errors);
        }



        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(String token, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user is not null)

            {

                var result = await userManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded)

                {

                    return Ok(new { message = "email confirmed" });

                } else

                {

                    return BadRequest(result.Errors);
                }
            }
            return NotFound();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] WebApplication2.DTO.Request.LoginRequest loginRequest)
        {
            var applicationUser = await userManager.FindByEmailAsync(loginRequest.Email);
            if (applicationUser != null)
            {
                var result = await signInManager.PasswordSignInAsync(applicationUser, loginRequest.Password, loginRequest.RememberMe, false);

                List<Claim> claims = new();

                claims.Add(new(ClaimTypes.Name, applicationUser.UserName));

                var userRoles = await userManager.GetRolesAsync(applicationUser);
                if (userRoles.Count > 0)
                {

                    foreach (var item in userRoles)
                    {
                        claims.Add(new(ClaimTypes.Role, item));
                    }
                }
                if (result.Succeeded)
                {

                    SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("b2PoI959Z00AcgyDmBU7K8lxI1LWIDV6"));
                    SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                    var jwtToken = new JwtSecurityToken(

                    claims: claims,

                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signingCredentials
                    );

                    string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                    return Ok(new { token });
                }
                else
                {
                    if (result.IsLockedOut)
                    {
                        return BadRequest(new { message = "your account is locked, please try again later." });

                    }
                    if (result.IsNotAllowed) {

                        return BadRequest(new { message = " email not confirmed, please confirm your email before logging." });
                    }
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
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {

            var applicationUser = await userManager.GetUserAsync(User);
            if (applicationUser != null)
            {
                var result = await userManager.ChangePasswordAsync(applicationUser, changePasswordRequest.OldPassword,
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



        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgotPassword(ForgetPassword request) {

            var applicationUser = await userManager.FindByEmailAsync(request.Email);

            if (applicationUser is not null)
            {

                var code = new Random().Next(1000, 9999).ToString();

                await passwordRestCodeService.AddAsync(new() {
                    AppllicationUserId = applicationUser.Id,
                    Code = code,
                    ExpirationCode = DateTime.Now.AddMinutes(30),

                });
                await emailSender.SendEmailAsync(applicationUser.Email, " ResetPassword",
                  $"<h1> Hello.. {applicationUser.UserName} </h1> <p> t-tshop, ResetPassword <p/> * " +
                  $" Code Is {code} ");

                return Ok(code);
            }

            else
            {

                return BadRequest(new { message = "email not found" });
            }
        }

        [HttpPatch("SendCode")]
        public async Task<IActionResult> SendCode([FromBody] SendCodeRequest request)
        {

            var appUser = await userManager.FindByEmailAsync(request.Email);

            if (appUser is not null)
            {

                var resetCode = (await passwordRestCodeService.GetAsync(e => e.ApplicationUserId == appUser.Id))

                .OrderByDescending(e => e.ExpirationCode).FirstOrDefault();

                if (resetCode is not null && resetCode.Code == request.Code && resetCode.ExpirationCode > DateTime.Now)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(appUser);

                    var result = await userManager.ResetPasswordAsync(appUser, token, request.password);

                    if (result.Succeeded) {

                        await emailSender.SendEmailAsync(appUser.Email, "password changed",

                          $"< h1 > Hello{appUser.UserName} </ h1 > < p > t - tshep, your password is changed < p /> ");

                        return Ok(new { message = "password has been changed successfully" });
                    }

                    else {
                        return BadRequest(result.Errors);
                    }
                }

                else
                {

                    return BadRequest(new { message = "invalid code" });
                }
            }
                return BadRequest(new { message = "user not found" });

            
            }
        }
    }
