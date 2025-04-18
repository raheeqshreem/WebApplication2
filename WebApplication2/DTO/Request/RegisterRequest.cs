using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;
using WebApplication2.Validation;
namespace WebApplication2.DTO.Request
{
    public class RegisterRequest
    {
        [MinLength(3)]
        public string FirstName { get; set; }

        [MinLength(5)]
        public string LastName { get; set; }

        [MinLength(6)]
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
       
        public string Password { get; set; }

        [Compare(nameof(Password),ErrorMessage ="Password do not natch")]
        public string ConfirmPassword { get; set; }
        public ApplicationUserGender Gender { get; set; }
        [Over18Years]
        public DateTime BirthOfDate { get; set; }


    }
}
