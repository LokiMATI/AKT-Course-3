using ConsoleApp.Contexts;
using ConsoleApp.Services;
using Microsoft.EntityFrameworkCore;

using var context = new AppDbContext();
FilmService filmService = new(context);
SessionService sessionService = new(context);


#region Task 1
Console.WriteLine("Task 1");

var films = await filmService.GetFilmsByOrderAsync("Year");
films.ForEach(f => Console.WriteLine($"{f.Title} {f.Year}"));
#endregion

#region Task 2
Console.WriteLine("\nTask 2");

var searchedFilms = await filmService.GetFilmsAsync("Оно", 2018);
await searchedFilms.ForEachAsync(f => Console.WriteLine($"{f.Title} {f.Year}"));
#endregion

#region Task 3
Console.WriteLine("\nTask 3");

Console.Write("Сумма для повышения стоимости: ");
decimal price = decimal.Parse(Console.ReadLine());
Console.Write("Номер зала: ");
int hallId = int.Parse(Console.ReadLine());

Console.WriteLine($"Количество изменнёных строк: {await sessionService.RaisSessionPriceAsync(price, hallId)}");
#endregion

#region Task 4
Console.WriteLine("\nTask 4");

Console.Write("Введите идентификатор фильма: ");
int filmId = int.Parse(Console.ReadLine());

var genres = await filmService.GetFilmGenresAsync(filmId);
foreach (var genre in genres)
    Console.WriteLine($"{genre.GenreId} {genre.Title}");
#endregion 
