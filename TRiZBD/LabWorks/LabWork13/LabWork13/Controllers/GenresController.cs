using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabLibrary.Contexts;
using LabLibrary.Models;

namespace LabWork13.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController(CinemaDbContext context) : ControllerBase
{
    private readonly CinemaDbContext _context = context;

    // GET: api/Genres
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
    {
        return await _context.Genres.ToListAsync();
    }

    // GET: api/Genres/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Genre>> GetGenre(int id)
    {
        var Genre = await _context.Genres.FindAsync(id);

        if (Genre == null)
        {
            return NotFound();
        }

        return Genre;
    }

    // PUT: api/Genres/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGenre(int id, Genre Genre)
    {
        if (id != Genre.GenreId)
        {
            return BadRequest();
        }

        _context.Entry(Genre).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!GenreExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Genres
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Genre>> PostGenre(Genre Genre)
    {
        _context.Genres.Add(Genre);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetGenre", new { id = Genre.GenreId }, Genre);
    }

    // DELETE: api/Genres/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var Genre = await _context.Genres.FindAsync(id);
        if (Genre == null)
        {
            return NotFound();
        }

        _context.Genres.Remove(Genre);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool GenreExists(int id)
    {
        return _context.Genres.Any(e => e.GenreId == id);
    }
}
