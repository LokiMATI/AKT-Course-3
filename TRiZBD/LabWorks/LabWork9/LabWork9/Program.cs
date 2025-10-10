using LabWork9.Contexts;
using LabWork9.Models;
using LabWork9.Services;

using var context = new AppDbContext();
var visitorService = new VisitorService(context);
var ticketService = new TicketService(context);

Console.WriteLine("Посетители");
var visitors = await visitorService.GetVisitorsAsync();
foreach (var entity in visitors)
    Console.WriteLine($"VisitorId: {entity.VisitorId} Phone: {entity.Phone} Name: {entity.Name} Birthday: {entity.Birthday} Email: {entity.Email}");

Console.WriteLine("\nБилеты");
var tickets = await ticketService.GetTicketsAsync();
foreach (var entity in tickets)
    Console.WriteLine($"TicketId: {entity.TicketId} SessionId: {entity.SessionId} Row: {entity.Row} Seat: {entity.Seat}");

var visitor = new Visitor()
{
    Phone = "0000000000",
    Name = "Никита",
    Email = "email@email.com"
};

await visitorService.AddVisitorAsync(visitor);

visitor.Name = "Матигоров Никита";
await visitorService.UpdateVisitorAsync(visitor);

var ticket = new Ticket()
{
    SessionId = 1,
    Row = 3,
    Seat = 7,
    Visitor = visitor
};

await ticketService.AddTicketAsync(ticket);

ticket.SessionId = 3;
await ticketService.UpdateTicketAsync(ticket);

await ticketService.DeleteTicketAsync(ticket.TicketId);
await visitorService.DeleteVisitorAsync(visitor.VisitorId);
