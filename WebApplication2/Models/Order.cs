using static System.Runtime.InteropServices.JavaScript.JSType;
using WebApplication2.Models;

namespace WebApplication2.Models
{
    public class Order
    {


        public enum OrderStatus {

            Pending,

            Cancelled,

            Approved,

            Shipped,

            Complete
        }


        public enum PaymentMethodType
        {
                Visa,Cash
        }




        public int Id { get; set; }


 public OrderStatus orderStatus { get; set; }

  

public DateTime OrderDate { get; set; }

   

public DateTime ShippedDate { get; set; }

    

public decimal TotalPrice { get; set; }

    //payment

 

public PaymentMethodType paymentMethodType { get; set; }



public string? SessionId { get; set; }



public string? TransactionId { get; set; }

//carrier



public string? Carrier { get; set; }


public string? TrackingNumber { get; set; }

//relation



public ApplicationUsr  ApplicationUser { get; set; }




public string ApplicationUserId { get; set; }





    }
}
