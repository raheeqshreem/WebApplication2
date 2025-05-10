﻿using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{


    [PrimaryKey(nameof(OrderId),nameof(ProductId))]
    public class OrderItem
    {

    
public int OrderId { get; set; }

     

public Order Order { get; set; }

    

public int ProductId { get; set; }

       
public Product Product { get; set; }

  

public decimal TotalPrice { get; set; }

        
public string? Note { get; set; }







        }
}
