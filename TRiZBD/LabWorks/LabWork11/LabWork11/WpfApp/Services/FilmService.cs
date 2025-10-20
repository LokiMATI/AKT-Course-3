using Microsoft.EntityFrameworkCore;
using WpfApp.Contexts;
using WpfApp.Models;

namespace WpfApp.Services;

public class FilmService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<List<Film>> GetFilmsAsync()
        => await _context.Films.ToListAsync();

    public async Task<List<string>> GetAgeRatingsAsync()
    {
        return await _context.Films.Select(f => f.AgeRating).Distinct().ToListAsync();
    }

    public async Task RemoveFilmAsync(int id)
    {
        var film = await _context.Films.FindAsync(id);
        if (film is null)
            return;

        _context.Films.Remove(film);
        await _context.SaveChangesAsync();
    }
}
