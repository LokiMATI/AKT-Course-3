using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.Helpers;
using CinemaDbLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaDbLibrary.Services;

public class VisitorService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<Visitor?> GetVisitorAsync(int id)
        => await _context.Visitors
                    .FirstOrDefaultAsync(v => v.VisitorId == id);

    public async Task<List<Visitor>> GetVisitorsAsync(Paginator? pagination = null)
    {
        var visitors = _context.Visitors.AsQueryable();

        if (pagination is not null)
            visitors = visitors
                .Skip(pagination.PageSize * pagination.PageNumber)
                .Take(pagination.PageSize);

        return await visitors.ToListAsync();
    }

    public async Task AddVisitorAsync(Visitor entity)
    {
        await _context.Visitors.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateVisitorAsync(Visitor entity)
    {
        _context.Visitors.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteVisitorAsync(int id) 
        => await _context.Visitors
            .Where(v => v.VisitorId == id)
            .ExecuteDeleteAsync();
}
