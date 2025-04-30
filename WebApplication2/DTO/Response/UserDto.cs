using WebApplication2.Models;

namespace WebApplication2.DTO.Response
{
    public class UserDto
    {
        public string? Id { get; set; }



        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public String Gender { get; set; }
        public DateTime BirthOfDate { get; set; }



    }
}
