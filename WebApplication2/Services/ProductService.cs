using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Expressions;
using WebApplication2.Data;
using WebApplication2.DTO.Request;
using WebApplication2.DTO.Response;
using WebApplication2.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using WebApplication2.Services.IServices;
using Microsoft.EntityFrameworkCore;
namespace WebApplication2.Services
{
    public class ProductService : Service<Product>,IProductServise
    {
        private readonly ApplicationDbContext dbContext;

        public ProductService(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AddAsync(ProductRequest request, CancellationToken cancellationToken = default)
        {
            var file = request.mainImg;
            var product = request.Adapt<Product>();
            if (file != null && file.Length > 0)
            {
                // /tetrirette.png  
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }

                product.mainImg = fileName;
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> Edit(int id, ProductRequest productRequest, CancellationToken cancellationToken = default)
        {
            var productInDb = dbContext.Products.FirstOrDefault(product => product.Id == id);

            if (productInDb == null)
            {
                return false;
            }

            var file = productRequest.mainImg;

            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }

                // Delete old image from folder
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "images", productInDb.mainImg);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                productInDb.mainImg = fileName;
            }

            // Update the other properties from the DTO
            productRequest.Adapt(productInDb); // Update properties from DTO to the tracked entity

            dbContext.SaveChanges();

            return true;
        }




        public async Task<bool> remove(int id, CancellationToken cancellationToken = default)
        {
            var product = await dbContext.Products.FindAsync(new object[] { id }, cancellationToken);

            if (product == null)
            {
                return false; // المنتج غير موجود
            }

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }








    }
}
