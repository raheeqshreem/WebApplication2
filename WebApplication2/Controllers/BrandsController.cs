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
        public IActionResult GetAll()
        {
            var brands= brandService.GetAll();
            return Ok(brands.Adapt<IEnumerable<BrandResponse>>());
        }

        [HttpGet("{id}")]

        public IActionResult GetById([FromRoute] int id)
        {
            var brand = brandService.Get(e => e.Id == id);

            if (brand == null)

                return NotFound();

            return Ok(brand.Adapt<BrandResponse>());



        }

        [HttpPost("")]
        public IActionResult Create([FromBody] BrandRequest brand)
        {
            var brandINAD = brandService.Add(brand.Adapt<Brand>());


            // return Created($"{Request.Scheme}://{Request.Host}/api/Categories/{category.Id}",category);
            return CreatedAtAction(nameof(GetById), new { brandINAD.Id }, brandINAD);
        }


        [HttpPut("{id}")]
        public IActionResult Update([FromBody] int id, [FromBody]BrandRequest brand)
        {
            var brandINDP = brandService.Edit(id, brand.Adapt<Brand>());
            if (!brandINDP)
                return NotFound();
            return NoContent();

        }


        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var brandINDP = brandService.remove(id);
            if (!brandINDP)
                return NotFound();
            return NoContent();

        }





    }
}
