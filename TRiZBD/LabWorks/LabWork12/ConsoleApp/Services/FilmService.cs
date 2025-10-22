using ConsoleApp.Contexts;
using ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ConsoleApp.Services;

public class FilmService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<List<Film>> GetFilmsByOrderAsync(string columnName) 
        => await _context.Films
        .FromSqlRaw($"SELECT * FROM Film ORDER BY {columnName}")
        .ToListAsync();

    public async Task<IQueryable<Film>> GetFilmsAsync(string title, short year)
        => await Task.Run(() => _context.Films.FromSql($"SELECT * FROM Film WHERE Title = {title} AND Year >= {year}"));

    public async Task<IEnumerable<Genre>> GetFilmGenresAsync(int id)
    {
        var film = await _context.Database.SqlQuery<Film>($"SELECT * FROM Film WHERE FilmId = {id})").FirstOrDefaultAsync();
        return film.Genres;
    }
}
