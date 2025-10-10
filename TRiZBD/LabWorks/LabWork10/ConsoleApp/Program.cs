using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.Helpers;
using CinemaDbLibrary.Models;
using CinemaDbLibrary.Services;

using var context = new AppDbContext();

var visitorService = new VisitorService(context);
var ticketService = new TicketService(context);
var filmService = new FilmService(context);
var genreService = new GenreService(context);

Console.WriteLine("\nБилеты");
var tickets = await ticketService.GetTicketsAsync();
foreach (var t in tickets)
    Console.WriteLine($"{t.TicketId} {t.SessionId} {t.Row} {t.Seat}");

Console.WriteLine("Посетители");
var visitors = await visitorService.GetVisitorsAsync();
foreach (var v in visitors)
    Console.WriteLine($"{v.VisitorId} {v.Phone} {v.Name} {v.Birthday} {v.Email}");

Console.WriteLine("\nФильмы");
var films = await filmService.GetFilmsAsync();
foreach (var f in films)
    Console.WriteLine($"{f.FilmId} {f.Title} {f.Duration} {f.Year} {f.Description} {f.AgeRating} {f.RentalStart} {f.RentalEnd} {f.IsDeleted}");

Console.WriteLine("\nЖанры");
var genres = await genreService.GetGenresAsync();
foreach (var g in genres)
    Console.WriteLine($"{g.GenreId} {g.Title}");

Console.WriteLine("\nПагинация");
Console.Write("Размер страницы: ");
var pageSize = int.Parse(Console.ReadLine());
Console.Write("Номер страницы: ");
var pageNumber = int.Parse(Console.ReadLine());

var paginator = new Paginator
{
    PageSize = pageSize,
    PageNumber = pageNumber
};

visitors = await visitorService.GetVisitorsAsync(paginator);

Console.WriteLine("\nПагинация посетителей: ");
foreach (var visitor in visitors)
    Console.WriteLine($"{visitor.VisitorId} {visitor.Phone} {visitor.Name} {visitor.Birthday} {visitor.Email}");

films = await filmService.GetFilmsAsync(paginator);
Console.WriteLine("\nПагинация фильмов: ");
foreach (var f in films)
    Console.WriteLine($"{f.FilmId} {f.Title} {f.Duration} {f.Year} {f.Description} {f.AgeRating} {f.RentalStart} {f.RentalEnd} {f.IsDeleted}");

Console.WriteLine("\nСортировка");
Console.Write("Название столбца: ");
var columnName = int.Parse(Console.ReadLine());
Console.Write("Сортировать по возрастанию или нет(y/n): ");
var isAsc = Console.ReadLine() == "y";

visitors = await visitorService.GetVisitorsAsync(paginator);

Console.WriteLine("\nПагинация посетителей: ");
foreach (var visitor in visitors)
    Console.WriteLine($"{visitor.VisitorId} {visitor.Phone} {visitor.Name} {visitor.Birthday} {visitor.Email}");

var sorter = new Sorter
{
    ColumnName = "Title"
};
films = await filmService.GetFilmsAsync(paginator, sorter);
Console.WriteLine("\nПагинация фильмов: ");
foreach (var f in films)
    Console.WriteLine($"{f.FilmId} {f.Title} {f.Duration} {f.Year} {f.Description} {f.AgeRating} {f.RentalStart} {f.RentalEnd} {f.IsDeleted}");