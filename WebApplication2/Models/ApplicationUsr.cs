using Microsoft.AspNetCore.Identity;

namespace WebApplication2.Models


{


    public enum ApplicationUserGender
    {
        Male,Female
    }
    public class ApplicationUsr : IdentityUser
    {

        public string? State { get; set; }



        public string? City { get; set; }



        public string? Street { get; set; }



        public string FirstName { get; set; }



        public string LastName { get; set; }



        public ApplicationUserGender Gender { get; set; }



        public DateTime BirthOfDate { get; set; }



    }  
}
