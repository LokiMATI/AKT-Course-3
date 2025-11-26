using WindowApp.Models;

namespace WindowApp.Services.SaveTicketManager;

public interface ISaveTicketManager
{
    public Task SaveTicketAsync(string path, TicketInfo ticket);
}
