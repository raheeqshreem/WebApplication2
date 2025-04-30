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
    public class ProductsController : ControllerBase
    {
        private readonly IProductServise productService;

        public ProductsController (IProductServise productService)
        {
            this.productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var product = await productService.GetOne(e => e.Id == id, null, true);

            if (product == null)

                return NotFound();

            return Ok(product.Adapt<ProductResponse>());
        }

        [HttpGet("GetByName/{Name}")]
        public async Task<IActionResult> GetByName([FromRoute] string Name)
        {
            var category = await productService.GetOne(e => e.Name.Equals(Name));

            if (category == null)

                return NotFound();

            return Ok(category.Adapt<ProductResponse>());
        }
        [HttpGet("")]
        public async Task<IActionResult> GetCategories()
        {
            var result = await productService.GetAsync();
            if (result != null)
            {
                return Ok(result.Adapt<IEnumerable<ProductResponse>>());
            }
            return NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] ProductRequest dto, CancellationToken cancellationToken)
        {
            var result = await productService.AddAsync(dto, cancellationToken);
            if (!result)
            {
                return BadRequest("فشل في إضافة المنتج");
            }
            return Ok("تمت إضافة المنتج بنجاح");
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] ProductRequest dto, CancellationToken cancellationToken)
        {
            var result = await productService.Edit(id, dto, cancellationToken);
            if (!result)
            {
                return NotFound("المنتج غير موجود أو حدث خطأ أثناء التعديل");
            }
            return Ok("تم تعديل المنتج بنجاح");
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await productService.remove(id, cancellationToken);
            if (!result)
            {
                return NotFound("المنتج غير موجود");
            }
            return Ok("تم حذف المنتج بنجاح");
        }

    }
}
