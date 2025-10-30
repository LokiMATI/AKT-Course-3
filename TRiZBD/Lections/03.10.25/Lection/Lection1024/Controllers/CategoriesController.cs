using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lection1024.Contexts;
using Lection1024.Models;

namespace Lection1024.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController(GamesDbContext context) : ControllerBase
    {
        private readonly GamesDbContext _context = context;

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories(
             [FromQuery]string? sortBy = null,
             [FromQuery]int? page = null)
        {
            var categories = _context.Categories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(sortBy))
                categories = sortBy?.ToLower() switch
                {
                    "name" => categories.OrderBy(x => x.Name),
                    "id" => categories.OrderBy(c => c.CategoryId),
                    _ => categories

                };

            if (page.HasValue)
            {
                var pageSize = 2;
            categories = categories
                .Skip(pageSize * ((int)page - 1))
                .Take(pageSize);

            }
            
            return await _context.Categories.ToListAsync();
        }

        [HttpGet("{category}/games")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames(string category)
            => await _context.Games
                .Where(g => g.Category.Name == category)
                .ToListAsync();

        [HttpGet("filtered")]
        public async Task<ActionResult<IEnumerable<Category>>> GetFiltratedCategories(string category)
            => await _context.Categories
                .Where(g => g.Name == category)
                .ToListAsync();


        // GET: api/Categories/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return category is null ? NotFound() : category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutCategory(
            [FromRoute] int id, 
            [FromBody] Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CategoryExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category is null)
                return NotFound();
            

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> CategoryExists(int id)
            => _context.Categories.Any(e => e.CategoryId == id);
    }
}
