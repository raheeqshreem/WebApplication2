using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.DTO.Request;
using WebApplication2.DTO.Response;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController (ICategoryService categoryService): ControllerBase
    {
     private readonly  ICategoryService categoryService = categoryService;

       



        [HttpGet("")]
        public IActionResult GetAll()
        {
            var categories = categoryService.GetAll();
            return Ok(categories.Adapt<IEnumerable<CategoryResponse>>()) ;
        }

        [HttpGet("{id}")]
      
        public IActionResult GetById([FromRoute] int id)
        {
            var category = categoryService.Get(e => e.Id == id);
          
            if (category == null)
            
                return NotFound();

            return Ok(category.Adapt<CategoryResponse>());



        }

        [HttpPost("")]
        public IActionResult Create([FromBody] CategoryRequest category)  {
            var categoryINAD = categoryService.Add(category.Adapt<Category>());


            // return Created($"{Request.Scheme}://{Request.Host}/api/Categories/{category.Id}",category);
            return CreatedAtAction(nameof(GetById), new {categoryINAD.Id},categoryINAD);
        }


        [HttpPut("{id}")]
        public IActionResult Update([FromBody] int id, [FromBody] CategoryRequest category)
        {
            var categoryINDP = categoryService.Edit(id, category.Adapt<Category>());
            if (!categoryINDP)
                return NotFound();
            return NoContent();

        }


            [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            var categoryINDP = categoryService.remove(id);
            if (!categoryINDP)
                return NotFound();
            return NoContent();

        }


    }
}
