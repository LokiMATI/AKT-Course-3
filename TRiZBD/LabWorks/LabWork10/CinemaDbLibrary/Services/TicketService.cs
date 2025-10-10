using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.Helpers;
using CinemaDbLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaDbLibrary.Services;

public class TicketService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<Ticket?> GetTicketAsync(int id)
        => await _context.Tickets
                    .Include(t => t.Visitor)
                    .FirstOrDefaultAsync(t => t.TicketId == id);

    public async Task<List<Ticket>> GetTicketsAsync(Sorter? sorter = null)
    {
        var tickets = _context.Tickets.AsQueryable();

        if (sorter is not null)
            tickets = sorter.IsAsc
                ? tickets.OrderBy(t => EF.Property<object>(t, sorter.ColumnName))
                : tickets.OrderByDescending(t => EF.Property<object>(t, sorter.ColumnName));

        return await tickets.ToListAsync();
    }

    public async Task AddTicketAsync(Ticket entity)
    {
        await _context.Tickets.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTicketAsync(Ticket entity)
    {
        _context.Tickets.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTicketAsync(int id) 
        => await _context.Tickets
            .Where(v => v.TicketId == id)
            .ExecuteDeleteAsync();
}
