using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTO.Request;
using WebApplication2.DTO.Response;
using WebApplication2.Models;
using WebApplication2.Services;
using Mapster;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class BrandsController (IBrandService brandService): ControllerBase
    {
        private readonly IBrandService brandService = brandService;


        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var brands = await brandService.GetAsync();
            return Ok(brands.Adapt<IEnumerable<CategoryResponse>>());
        }


        [HttpGet("{id}")]

        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var brand = await brandService.GetOne(e => e.Id == id);

            if (brand == null)

                return NotFound();

            return Ok(brand.Adapt<BrandResponse>());
        }




        [HttpPost("")]
        public async Task<IActionResult> CreateAsync([FromBody] BrandRequest brand, CancellationToken cancellationToken)
        {
            var brandINAD = await brandService.AddAsync(brand.Adapt<Brand>(), cancellationToken);


            // return Created($"{Request.Scheme}://{Request.Host}/api/Categories/{category.Id}",category);
            return CreatedAtAction(nameof(GetByIdAsync), new { brandINAD.Id }, brandINAD);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] int id, [FromBody] BrandRequest brand)
        {
            var brandINDP = await brandService.EditAsync(id, brand.Adapt<Brand>());
            if (!brandINDP)
                return NotFound();
            return NoContent();

        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var brandINDP = await brandService.removeAsync(id);
            if (!brandINDP)
                return NotFound();
            return NoContent();

        }









    }
}
