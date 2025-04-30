using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication2.DTO.Response;
using WebApplication2.Models;
using WebApplication2.Services;
using WebApplication2.Utility;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles =$"{StaticData.SuperAdmin}")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

public UsersController  (IUserService userService)
        {

            this.userService = userService;
        }



        [HttpGet("")]

        public async Task<ActionResult> GetAll()
        {

            var users = await userService.GetAsync();

            return Ok(users.Adapt< IEnumerable<UserDto>>());
        }


        [HttpGet("{id}")]


        public async Task<IActionResult> Get(string id)

        {

            var user = await userService.GetOne(u => u.Id == id);

            return Ok(user.Adapt<UserDto>());
        }


        [HttpPut("{userId}")]
        public async Task<IActionResult> ChangeRole([FromRoute] string userId, [FromQuery] string newRoleName)
        {

            var result = await userService.ChangeRole(userId, newRoleName);

            return Ok(result);
        }





    }
}
