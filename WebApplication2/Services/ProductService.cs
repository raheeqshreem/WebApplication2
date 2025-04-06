using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Expressions;
using WebApplication2.Data;
using WebApplication2.DTO.Request;
using WebApplication2.DTO.Response;
using WebApplication2.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Mapster;
using Microsoft.AspNetCore.Hosting;
namespace WebApplication2.Services
{
    public class ProductService : IProductServise
    {

        ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }


      
        public ProductResponse?Add(ProductRequest productRequest)
        {
            var file = productRequest.mainImg;
            var product = productRequest.Adapt<Product>();

            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);

                // Save file to server
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }

                // Assign the file name to the product object
                product.mainImg = fileName;

                _context.Products.Add(product);
                _context.SaveChanges();

                return product.Adapt<ProductResponse>(); // Return a response if necessary
            }

            return null; // Or handle the error accordingly
        }



















        public ProductResponse? Get(int id)
        {
            var product = _context.Products.Find(id);

            // إذا لم يوجد المنتج، إرجاع null أو يمكن إرجاع رسالة أخرى مثل NotFound
            if (product == null)
            {
                return null;
            }

            // تحويل المنتج إلى ProductResponse
            return product.Adapt<ProductResponse>();
        }




        public IEnumerable<ProductResponse>GetAll()
        {
            // جلب جميع المنتجات من قاعدة البيانات
            var products = _context.Products.ToList();

            // إذا كانت المنتجات فارغة، يمكن إرجاع null أو قائمة فارغة
            if (products == null || !products.Any())
            {
                return null;
            }

            // تحويل المنتجات إلى ProductResponse
            return products.Adapt<IEnumerable<ProductResponse>>();
        }
}










       public bool remove(int id)
        {
            var product = _context.Products.Find(id);

            // إذا لم يتم العثور على المنتج، إرجاع false
            if (product == null)
            {
                return false;
            }

            // تحديد مسار الصورة وحذفها إذا كانت موجودة
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", product.mainImg);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            // إزالة المنتج من قاعدة البيانات
            _context.Products.Remove(product);
            _context.SaveChanges();

            return true;
        }





    }
}
