using Lection1007.Contexts;
using Lection1007.Filters;
using Lection1007.Models;
using Microsoft.EntityFrameworkCore;

namespace Lection1007.Services;

public class GameService(StoreDbContext context)
{
    private readonly StoreDbContext _context = context;

    public async Task<List<Game>> GetGamesAsync(GameFilter? filter)
    {
        if (filter is null)
            return await _context.Games.ToListAsync();

        var games = context.Games.AsQueryable();
        if (filter.Price is not null)
            games = games.Where(g => g.Price < filter.Price);
        if (filter.Name is not null)
            games = games.Where(g => g.Name == filter.Name);
        if (filter.Category is not null)
            games = games.Where(g => g.Category.Name == filter.Category);

        return await games.ToListAsync();
    }
}
