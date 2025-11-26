using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using WindowApp.Contexts;
using WindowApp.Models;
using WindowApp.Services.SaveTicketManager;
using WindowApp.Services.SaveTicketManagers;

namespace WindowApp.Services;

public class TicketService(TicketDbContext context)
{
    private readonly TicketDbContext _context = context;

    public async Task<TicketInfo> GetTicketInfoAsync(int id)
    {
        var ticket = await _context.Tickets
            .Include(t => t.Session)
            .ThenInclude(s => s.Film)
            .Select(t => new TicketInfo() 
            { 
                Id = id,
                Title = t.Session.Film.Title,
                SeesionTime = t.Session.SessionTime,
                Cinema = t.Session.Hall.Cinema,
                HallId = t.Session.HallId,
                Row = t.Row,
                Seat = t.Seat
            })
            .FirstOrDefaultAsync(t => t.Id == id);

        if (ticket is null)
            throw new Exception("Билета с таким номером нет.");

        return ticket;
    }

    public async Task<bool> SaveTicketAsync(TicketInfo ticket)
    {
        SaveFileDialog dialog = new();

        dialog.Filter = "TXT (*.txt)|*.txt|PDF (*.pdf)|*.pdf";
        dialog.FileName = $"Ticket №{ticket.Id}";

        if (dialog.ShowDialog() == true)
        {
            ISaveTicketManager saveTicketManager;
            switch (dialog.FilterIndex)
            {
                case 0:
                    saveTicketManager = new TxtSaveTicketManager();
                    break;

                case 1:
                    saveTicketManager = new PdfSaveTicketManager();
                    break;

                default:
                    saveTicketManager = new TxtSaveTicketManager();
                    break;
            }

            await saveTicketManager.SaveTicketAsync(dialog.FileName, ticket);

            return true;
        }

        return false;
    }
}
