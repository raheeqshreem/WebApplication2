﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ApplicationDbContext _context;
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public IActionResult GetAll()
        {
            var categories = _context.Categories.ToList();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var category = _context.Categories.Find(id);
            if(category == null)
            
                return NotFound();

            return Ok(category);



        }
        [HttpPost ("")]
        public IActionResult Create([FromBody] Category category)  {
            _context.Categories.Add(category);
            _context.SaveChanges();
            // return Created($"{Request.Scheme}://{Request.Host}/api/Categories/{category.Id}",category);
            return CreatedAtAction(nameof(GetById), new {category.Id},category);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            var category = _context.Categories.Find(id);
            if(category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);

            _context.SaveChanges();
            return NoContent();

        }


    }
}
