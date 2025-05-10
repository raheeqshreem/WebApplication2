using System;

namespace WebApplication2.Models
{
    public class PasswordResetCode
    {

        public int Id { get; set; }

       

       public string ApplicationUserId { get; set; }

       

       public ApplicationUsr ApplicationUser { get; set; }

       

         public string code{get ; set ; }
   

        public DateTime ExpirationCode { get; set; }


    }
}
