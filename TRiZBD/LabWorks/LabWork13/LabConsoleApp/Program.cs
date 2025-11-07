using ApiServicesLibrary;
using LabLibrary.Models;

HttpClient client = new();
client.BaseAddress = new("http://localhost:5012/api/");

FilmService filmService = new(client);
GenreService genreService = new(client);
VisitorService visitorService = new(client);

Console.WriteLine("Задание 2.2 (чтение)");
var films = await filmService.GetFilmsAsync();

films.ForEach(f => Console.WriteLine(f.Title));

Console.WriteLine("\nЗадание 2.2 (запись)");
Film film = new()
{
    Title = "Test",
    Description = "Test",
    Year = 2000
};
film = await filmService.AddFilmAsync(film);
Console.WriteLine(film.FilmId);

Console.WriteLine("\nЗадание 3.1");
var genres = await genreService.GetGenresAsync();
genres.ForEach(g => Console.WriteLine(g.Title));

Console.WriteLine("\nЗадание 3.2");
Genre genre = new()
{
    Title = "Test"
};

genre = await genreService.AddGenreAsync(genre);

Console.WriteLine(genre.GenreId);

Console.WriteLine("\nЗадание 4.1");
var visitor = await visitorService.GetVisitorAsync(100);
Console.WriteLine(visitor is null ? "null" : visitor.Name);

Console.WriteLine("\nЗадание 4.2");
var visitors = await visitorService.GetVisitorsAsync();
visitors.ForEach(v => Console.WriteLine(v.Phone));

Console.WriteLine("\nЗадание 5.1");
visitor = await visitorService.AddVisitorAsync(new() { Name = "Test", Phone = "8005553536"});
Console.WriteLine(visitor.VisitorId);

Console.WriteLine("\nЗадание 5.2");
visitor.Name = "Test_Edited";
var status = await visitorService.EditVisitorAsync(visitor);
Console.WriteLine(status ? "Посетитель обновлён" : "Посетитель не найден.");

Console.ReadLine();