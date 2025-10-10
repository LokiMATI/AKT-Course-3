using LabWork9.Contexts;
using LabWork9.Models;
using Microsoft.EntityFrameworkCore;

namespace LabWork9.Services;

public class VisitorService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<List<Visitor>> GetVisitorsAsync()
        => await _context.Visitors.ToListAsync();

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
