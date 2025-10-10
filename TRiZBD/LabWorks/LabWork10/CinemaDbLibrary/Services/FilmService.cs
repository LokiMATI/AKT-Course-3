using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.Helpers;
using CinemaDbLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace CinemaDbLibrary.Services;

public class FilmService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<Film?> GetFilmAsync(int id)
        => await _context.Films
                    .Include(f => f.Genres)
                    .FirstOrDefaultAsync(f => f.FilmId == id);

    public async Task<List<Film>> GetFilmsAsync(Paginator? pagination = null, Sorter? sorter = null)
    {
        var films = _context.Films.AsQueryable();

        if (sorter is not null)
            films = sorter.IsAsc
                ? films.OrderBy(f => EF.Property<object>(f, sorter.ColumnName))
                : films.OrderByDescending(f => EF.Property<object>(f, sorter.ColumnName));

        if (pagination is not null)
                films = films
                    .Skip(pagination.PageSize * pagination.PageNumber)
                    .Take(pagination.PageSize);

        return await films.ToListAsync();
    }

    public async Task AddFilmAsync(Film entity)
    {
        await _context.Films.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateFilmAsync(Film entity)
    {
        _context.Films.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteFilmAsync(int id) 
        => await _context.Films
            .Where(v => v.FilmId == id)
            .ExecuteDeleteAsync();
}
