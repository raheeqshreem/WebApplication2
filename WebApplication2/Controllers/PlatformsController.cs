using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Servises;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        Ios s;
        public PlatformsController(Ios os)
        {
            this.s = os;

        }




        [HttpGet]
        public IActionResult Get()
        {
            return Ok(s.RunService());
        }




    }
}
