using System.ComponentModel.DataAnnotations;

namespace WebApplication2.DTO.Request
{
    public class LoginRequest
    {

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
        public bool RememberMe{ get; set; }

    }
}
