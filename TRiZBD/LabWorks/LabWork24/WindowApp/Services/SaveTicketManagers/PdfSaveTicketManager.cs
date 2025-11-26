using WindowApp.Models;
using Word = Microsoft.Office.Interop.Word;
using WindowApp.Services.SaveTicketManager;

namespace WindowApp.Services.SaveTicketManagers;

public class PdfSaveTicketManager : ISaveTicketManager
{
    public async Task SaveTicketAsync(string path, TicketInfo ticket)
    {
        var wordApp = new Word.Application();
        wordApp.Visible = false;
        var document = wordApp.Documents.Add();

        PdfDocument pdf = renderer.RenderMarkdownStringAsPdf(@$"Билет № {ticket.Id}
{ticket.Title}
Начало сеанса: {ticket.SeesionTime.ToString("hh:mm dd MMMM")}
Кинотеатр: {ticket.Cinema}
Зал: {ticket.HallId}
Ряд: {ticket.Row} Место: {ticket.Seat}");

        document.SaveAs(path, Word.WdSaveFormat.wdFormatPDF);
    }
}
