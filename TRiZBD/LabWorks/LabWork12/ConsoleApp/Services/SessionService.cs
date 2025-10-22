using ConsoleApp.Contexts;
using ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.Services;

public class SessionService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<int> RaisSessionPriceAsync(decimal price, int hallId)
        => await _context.Database.ExecuteSqlAsync($"UPDATE Session SET Price += {price} WHERE HallId = {hallId}");
}
