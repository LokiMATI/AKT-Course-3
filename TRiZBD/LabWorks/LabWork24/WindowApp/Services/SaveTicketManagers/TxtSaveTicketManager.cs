using System.IO;
using System.Windows.Documents.Serialization;
using WindowApp.Models;

namespace WindowApp.Services.SaveTicketManager;

public class TxtSaveTicketManager : ISaveTicketManager
{
    public async Task SaveTicketAsync(string path, TicketInfo ticket)
    {
        using StreamWriter writer = new(path, false);

        await writer.WriteAsync(@$"Билет № {ticket.Id}
{ticket.Title}
Начало сеанса: {ticket.SeesionTime.ToString("hh:mm dd MMMM")}
Кинотеатр: {ticket.Cinema}
Зал: {ticket.HallId}
Ряд: {ticket.Row} Место: {ticket.Seat}");
    }
}
