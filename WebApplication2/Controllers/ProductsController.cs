using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using WebApplication2.Data;
using WebApplication2.DTO.Request;
using WebApplication2.DTO.Response;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductServise productService) : ControllerBase
    {
        private readonly IProductServise productService= productService;

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var products = productService.GetAll();

            if (products == null || !products.Any())
            {
                return NotFound(); // إذا لم تكن هناك منتجات
            }

            return Ok(products); // إرجاع قائمة المنتجات المحولة
        }






       [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            // استدعاء الـ GetById من الـ ProductService
            var product = productService.Get(id);

            if (product == null)
            {
                return NotFound(); // إرجاع 404 إذا لم يوجد المنتج
            }

            return Ok(product); // إرجاع المنتج المحول إلى ProductResponse
        }







        [HttpPost("")]
        public IActionResult Create([FromForm] ProductRequest productRequest)
        {
            // استدعاء دالة Create من الـ ProductService
            var result = productService.Add(productRequest);

            // إذا لم يتم إنشاء المنتج بنجاح، إرجاع BadRequest
            if (result == null)
            {
                return BadRequest();
            }

            // إرجاع المنتج المضاف مع الـ HTTP status Created
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }



        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            // استدعاء دالة Delete من الـ ProductService
            var result = productService.remove(id);

            // إذا كانت نتيجة الحذف false، إرجاع NotFound
            if (!result)
            {
                return NotFound(); // المنتج غير موجود
            }

            // إذا تم الحذف بنجاح، إرجاع NoContent
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromForm] ProductUpdateRequest productRequest)
        {

            var productInDb =_context.Products.AsNoTracking().FirstOrDefault(product=> product.Id == id);

            var product =productRequest.Adapt<Product>();

            var file= productRequest.mainImg;

            if (productInDb != null)
            {

                if (file != null && file.Length > 0)
                {

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);
                    using (var strean = System.IO.File.Create(filePath))
                    {

                        file.CopyTo(strean);
                    }

                    //delete old image from folder

                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "images", productInDb.mainImg);

                    if (System.IO.File.Exists(oldPath))
                    {

                        System.IO.File.Delete(oldPath);
                    }
                    product.mainImg = fileName;
                }

                else
                {

                    product.mainImg = productInDb.mainImg;
                }

                product.Id = id;

                _context.Products.Update(product);

                _context.SaveChanges();

                return NoContent();
            }






            }



        }
}
