using LabWork9.Contexts;
using LabWork9.Models;
using Microsoft.EntityFrameworkCore;

namespace LabWork9.Services;

public class TicketService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<List<Ticket>> GetTicketsAsync()
        => await _context.Tickets.ToListAsync();

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
