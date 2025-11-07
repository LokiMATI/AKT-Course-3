using LabLibrary.Contexts;
using LabLibrary.Models;
using LabWork13.DTOs;
using LabWork13.DTOs.DTO_Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabWork13.Controllers;

[Route("api/Films")]
[ApiController]
public class FilmsController(CinemaDbContext context) : ControllerBase
{
    private readonly CinemaDbContext _context = context;

    // GET: api/Films
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Film>>> GetFilms()
    {
        return await _context.Films.ToListAsync();
    }

    // GET: api/Films/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Film>> GetFilm(int id)
    {
        var film = await _context.Films.FindAsync(id);

        if (film == null)
        {
            return NotFound();
        }

        return film;
    }

    [HttpGet("pages")]
    public async Task<ActionResult<IEnumerable<Film>>> GetFilmsWithPagination(int page, string? sortBy = null)
    {
        var films = _context.Films.AsQueryable();

        if (!string.IsNullOrWhiteSpace(sortBy))
            films = sortBy?.ToLower() switch
            {
                "title" => films.OrderBy(x => x.Title),
                "year" => films.OrderBy(c => c.Year),
                "yeardesc" => films.OrderByDescending(c => c.Year),
                _ => films
            };

        int pageSize = 3;

        films = films
            .Skip(pageSize * (page - 1))
            .Take(pageSize);

        return await films.ToListAsync();
    }

    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<Film>>> GetFilmsWithFilter(int? year = null, string? title = null)
    {
        var films = _context.Films.AsQueryable();

        if (year.HasValue)
            films = films.Where(f => f.Year == year);

        if (!string.IsNullOrWhiteSpace(title))
            films = films.Where(f => f.Title.Contains(title));

        return await films.ToListAsync();
    }

    [HttpGet("{id}/Genres")]
    public async Task<ActionResult<IEnumerable<Genre>>> GetGenresByFilm(int id)
    {
        var film = await _context.Films
            .Include(f => f.Genres)
            .FirstOrDefaultAsync(f => f.FilmId == id);

        if (film is null)
            return NotFound();

        return film.Genres.ToList();
    }

    [HttpGet("{id}/sessions")]
    public async Task<ActionResult<IEnumerable<Session>>> GetFutureSessionsByFilm(int id)
    {
        var film = await _context.Films
            .Include(f => f.Sessions)
            .FirstOrDefaultAsync(f => f.FilmId == id);

        if (film is null)
            return NotFound();

        return film.Sessions.Where(s => s.SessionTime > DateTime.Now).ToList();
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Film>>> GetFilmsWithSearch(string? year = null, string? GenreNames = null)
    {
        var films = _context.Films.AsQueryable();

        if (!string.IsNullOrWhiteSpace(year))
        {
            string[] years = year.Split('-', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            int minYear, maxYear;
            if (!int.TryParse(years[0], out minYear) || !int.TryParse(years[1], out maxYear))
                return BadRequest();

            films = films.Where(f => f.Year >= minYear && f.Year <= maxYear);
        }

        if (!string.IsNullOrWhiteSpace(GenreNames))
        {
            string[] GenreValues = GenreNames.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            var Genres = await _context.Genres.Where(g => GenreValues.Contains(g.Title)).ToListAsync();
            foreach (var Genre in Genres)
                films = films.Where(f => f.Genres.Contains(Genre));
        }

        return await films.ToListAsync();
    }

    [HttpGet("statistics")]
    public async Task<ActionResult<IEnumerable<FilmDto>>> GetFilmDtos()
    {
        var films = await _context.Films
            .Include(f => f.Sessions)
            .ThenInclude(s => s.Tickets)
            .GroupBy(f => f.FilmId)
            .Select(g => new {g.Key, 
                TicketsCount = g.Sum(f => f.Sessions.Sum(s => s.Tickets.Count() * s.Price),
                PriceCount = g.Sum(f => f.Sessions.Sum())
            .ToListAsync();
        List<FilmDto> dtos = new();

        foreach (var film in films)
            dtos.Add(await GetDtoAsync(film));

        return dtos;
    }

    [HttpGet("statistics/{id}")]
    public async Task<ActionResult<FilmDto>> GetFilmDto(int id)
    {
        var film = await _context.Films.FirstOrDefaultAsync(f => f.FilmId == id);
        if (film is null)
            return NotFound();

        return await GetDtoAsync(film);
    }

    // PUT: api/Films/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFilm(int id, Film film)
    {
        if (id != film.FilmId)
        {
            return BadRequest();
        }

        _context.Entry(film).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FilmExists(id))
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

    // POST: api/Films
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Film>> PostFilm(Film film)
    {
        _context.Films.Add(film);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetFilm", new { id = film.FilmId }, film);
    }

    // DELETE: api/Films/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFilm(int id)
    {
        var film = await _context.Films.FindAsync(id);
        if (film == null)
        {
            return NotFound();
        }

        _context.Films.Remove(film);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool FilmExists(int id)
    {
        return _context.Films.Any(e => e.FilmId == id);
    }

    private async Task<FilmDto> GetDtoAsync(Film film)
    {
        int ticketAmout = 0;
        decimal salesProfit = 0;

        var sessions = await _context.Sessions.Where(s => s.FilmId == film.FilmId).ToListAsync();
        foreach (var session in sessions)
        {
            ticketAmout += _context.Tickets.Count(t => t.SessionId == session.SessionId);
            salesProfit += ticketAmout * session.Price;
        }

        return film.ToDto(ticketAmout, salesProfit);
    }
}
