using Microsoft.AspNetCore.DataProtection.Internal;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUsr>
    {
        
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Cart>().HasKey(e => new { e.ProductId, e.ApplicationUserId });
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Cart> Carts {get; set; }

        public DbSet<Order> Orders{ get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ResetPasswordRequest> ResetPasswordRequest { get; set; }


        // public IActionResult get()


    }
}
