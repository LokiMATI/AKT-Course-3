using LabLibrary.Contexts;
using LabLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VisitorsController : ControllerBase
{
    private readonly CinemaDbContext _context;

    public VisitorsController(CinemaDbContext context)
    {
        _context = context;
    }

    // GET: api/Visitors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Visitor>>> GetVisitors()
    {
        var visitors = await _context.Visitors.ToListAsync();

        if (visitors.Count() == 0)
            return NoContent();

        return visitors;
    }

    // GET: api/Visitors/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Visitor>> GetVisitor(int id)
    {
        var Visitor = await _context.Visitors.FindAsync(id);

        if (Visitor == null)
        {
            return NotFound();
        }

        return Visitor;
    }

    // PUT: api/Visitors/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutVisitor(int id, Visitor Visitor)
    {
        if (id != Visitor.VisitorId)
        {
            return BadRequest();
        }

        _context.Entry(Visitor).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!VisitorExists(id))
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

    // POST: api/Visitors
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Visitor>> PostVisitor(Visitor Visitor)
    {
        _context.Visitors.Add(Visitor);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetVisitor", new { id = Visitor.VisitorId }, Visitor);
    }

    // DELETE: api/Visitors/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVisitor(int id)
    {
        var Visitor = await _context.Visitors.FindAsync(id);
        if (Visitor == null)
        {
            return NotFound();
        }

        _context.Visitors.Remove(Visitor);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool VisitorExists(int id)
    {
        return _context.Visitors.Any(e => e.VisitorId == id);
    }
}
