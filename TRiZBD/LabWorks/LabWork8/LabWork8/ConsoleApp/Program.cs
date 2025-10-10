using ConsoleApp.DataBaseContexts;
using ConsoleApp.Repositories.Implementations;
using ConsoleApp.Models;


Console.WriteLine("Задание 1");
DatabaseContext dbContext = new("mssql", "ispp3113", "ispp3113", "3113");
VisitorRepository visitorRepository = new(dbContext);
GenreRepository genreRepository = new(dbContext);

using var connection = dbContext.CreateConnection();

try
{
    connection.Open();
    Console.WriteLine("Соединение есть.");
}
catch (Exception)
{
    Console.WriteLine("Соединения нет.");
}

//Console.WriteLine("\nЗадание 3");
//Console.Write("Идентификатор жанра: ");
//int genreId = int.Parse(Console.ReadLine());
//var genre = await genreRepository.GetByIdAsync(genreId);
//Console.WriteLine(genre is not null ? $"Название жанра: {genre.Title}\n" : "Жанр не был найден.\n");

//Console.Write("Идентификатор посетителя: ");
//int visitorId = int.Parse(Console.ReadLine());
//var visitor = await visitorRepository.GetByIdAsync(visitorId);
//Console.WriteLine(visitor is not null ? $"Номер телефона посетителя: {visitor.Phone}\n" : "Пользователь не был найден.\n");

//var genres = await genreRepository.GetAllAsync();
//foreach (var element in genres)
//{
//    Console.WriteLine($"Идентификатор: {element.GenreId}; Название: {element.Title}");
//}
//Console.WriteLine();

//var visitors = await visitorRepository.GetAllAsync();
//foreach (var element in visitors)
//{
//    Console.WriteLine($"Идентификатор: {element.VisitorId}; Телефон: +7{element.Phone};" +
//        $" Имя: {element.Name}; Дата рождения: {element.Birthday}; Email: {element.Email}");
//}

Console.WriteLine("\nЗадание 4");
Console.Write("Название жанра: ");
string title = Console.ReadLine();
Console.WriteLine($"Идентификатор нового жанра: {await genreRepository.AddAsync(new Genre() { Title = title})}");