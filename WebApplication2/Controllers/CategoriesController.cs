using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.DTO.Request;
using WebApplication2.DTO.Response;
using WebApplication2.Models;
using WebApplication2.Services;
using WebApplication2.Utility;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles =$"{StaticData.SuperAdmin}")]
    public class CategoriesController (IOrderService categoryService): ControllerBase
    {
     private readonly  IOrderService categoryService = categoryService;

       



        [HttpGet("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await categoryService.GetAsync();
            return Ok(categories.Adapt<IEnumerable<CategoryResponse>>()) ;
        }

        [HttpGet("{id}")]
      
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var category = await categoryService.GetOne(e => e.Id == id);
          
            if (category == null)
            
                return NotFound();

            return Ok(category.Adapt<CategoryResponse>());



        }

        [HttpPost("")]
        public async Task<IActionResult> CreateAsync([FromBody] CategoryRequest category,CancellationToken cancellationToken)  {
            var categoryINAD = await categoryService.AddAsync(category.Adapt<Category>(),cancellationToken);


            // return Created($"{Request.Scheme}://{Request.Host}/api/Categories/{category.Id}",category);
            return CreatedAtAction(nameof(GetByIdAsync), new {categoryINAD.Id},categoryINAD);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] int id, [FromBody] CategoryRequest category)
        {
            var categoryINDP =  await categoryService.EditAsync(id, category.Adapt<Category>());
            if (!categoryINDP)
                return NotFound();
            return NoContent();

        }


            [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int id)
        {
            var categoryINDP =  await categoryService.removeAsync(id);
            if (!categoryINDP)
                return NotFound();
            return NoContent();

        }


    }
}
