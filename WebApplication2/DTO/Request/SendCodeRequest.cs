using System.ComponentModel.DataAnnotations;

namespace WebApplication2.DTO.Request
{
    public class SendCodeRequest
    {
        public string Email { get; set; }
        public string password{ get; set; }

        public string Code { get; set; }
        [Compare(nameof(password))]
        public string ConfirmPassword { get; set; }


    }
}
