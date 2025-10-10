using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaDbLibrary.Services;

public class GenreService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<Genre?> GetGenreAsync(int id)
        => await _context.Genres
                    .Include(g => g.Films)
                    .FirstOrDefaultAsync(g => g.GenreId == id);

    public async Task<List<Genre>> GetGenresAsync()
        => await _context.Genres.ToListAsync();

    public async Task AddGenreAsync(Genre entity)
    {
        await _context.Genres.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateGenreAsync(Genre entity)
    {
        _context.Genres.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteGenreAsync(int id) 
        => await _context.Genres
            .Where(v => v.GenreId == id)
            .ExecuteDeleteAsync();
}
