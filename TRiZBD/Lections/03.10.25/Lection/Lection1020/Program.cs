using Lection1020.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

Console.WriteLine("Выполнение SQL-запросов средствами ORM");

using var context = new GamesDbContext();

int minPrice = 100;
int maxPrice = 100;
var games = context.Games
    .FromSql($"SELECT * FROM dbo.GetGamesByPrices({minPrice}, {maxPrice})");

Console.WriteLine();

//var id = new SqlParameter("@id", SqlDbType.Int)
//{
//    Direction = ParameterDirection.Output
//};

//string category = "arcada2";
//context.Database
//    .ExecuteSqlRaw($"dbo.AddCategory {category}, @id OUTPUT", id);

//Console.WriteLine(id);
//int price = 1000;
//var games = context.Games
//    .FromSql($"dbo.GetGamesByPrice {price}");

//var games = context.Games.Where(g => EF.Functions.Like(g.Name, "[a-d]%"));

//SqlQuery(context);
//await FromSql(context);

static async Task FromSql(GamesDbContext context)
{
    var games = context.Games
        .FromSql($"select * from game");
    Console.WriteLine(games.ToQueryString());
    await games.ForEachAsync(g =>
        Console.WriteLine($"{g.Name} {g.Price} {g.CategoryId}"));

    Console.WriteLine();

    string columnName = "price";
    int price = 1_000;
    games = context.Games
        .FromSql($"select * from game where price < {price}");
    Console.WriteLine(games.ToQueryString());
    await games.ForEachAsync(g =>
        Console.WriteLine($"{g.Name} {g.Price} {g.CategoryId}"));

    Console.WriteLine();

    string title = "SimCity";
    games = context.Games
        .FromSqlRaw($"select * from game where name = '{title}'");
    Console.WriteLine(games.ToQueryString());
    await games.ForEachAsync(g =>
        Console.WriteLine($"{g.Name} {g.Price} {g.CategoryId}"));

    Console.WriteLine();

    title = "SimCity";
    games = context.Games
        .FromSqlRaw($"select * from game where name = @p0", title);
    Console.WriteLine(games.ToQueryString());
    await games.ForEachAsync(g =>
        Console.WriteLine($"{g.Name} {g.Price} {g.CategoryId}"));

    Console.WriteLine();

    var sqlTitle = new SqlParameter("@title", "SimCity");
    games = context.Games
        .FromSqlRaw($"select * from game where name = @title", sqlTitle);
    Console.WriteLine(games.ToQueryString());
    await games.ForEachAsync(g =>
        Console.WriteLine($"{g.Name} {g.Price} {g.CategoryId}"));
}

static void SqlQuery(GamesDbContext context)
{
    var titles = context.Database
        .SqlQuery<string>($"select name from game");
    Console.WriteLine(titles.ToQueryString());

    Console.WriteLine();

    var minPrice = context.Database.SqlQuery<decimal>($"select min(price) as value from game").FirstOrDefault();

    Console.WriteLine();
}