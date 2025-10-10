using Lection1007.Contexts;
using Lection1007.DTOs;
using Lection1007.Models;
using Lection1007.Services;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Применение ORM");

var optionsBuilder = new DbContextOptionsBuilder<StoreDbContext>();
optionsBuilder.UseSqlServer("Data Source=mssql;Initial Catalog=ispp3113;User ID=ispp3113;Password=3113;Trust Server Certificate=True");

using var context = new StoreDbContext(optionsBuilder.Options);

var categoryService = new CategoryService(context);

var titles = context.Games
    .Select(g => g.Name);

foreach (var title in titles)
{
    Console.WriteLine(title);
}

var games = context.Games
    .Include(g => g.Category)
    .Select(g => g.ToDto());

var testGames = context.Games.ToList();
var dtos = testGames.

foreach (var g in games)
{
    Console.WriteLine($"{g.Title} {g.Price} {g.Tax} {g.Category}");
}

//Sort(context);

//using var context = new AppDbContext();

//context.Games.Where(g => g.GameId > 4).ExecuteDelete();

//UpdateCategory(context);

//UpdateCategoryFromDb(context);

//await RemoveCategory(context);

//await AddCategory(context);

//GetList(context);

static void GetList(AppDbContext context)
{
    var categories = context.Categories;
    foreach (var category in categories)
        Console.WriteLine($"{category.CategoryId} {category.Name}");

    Console.WriteLine();

    var games = context.Games;
    foreach (var game in games)
        Console.WriteLine($"{game.CategoryId} {game.Name}");
}

static async Task AddCategory(AppDbContext context)
{
    var category = new Category()
    {
        Name = "Casual"
    };
    await context.Categories.AddAsync(category);
    await context.SaveChangesAsync();
}

static async Task RemoveCategory(AppDbContext context)
{
    var category = await context.Categories.FindAsync(6);

    if (category is not null)
    {
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
    }
}

static void UpdateCategoryFromDb(AppDbContext context)
{
    var category = context.Categories.Find(1);
    if (category is null)
        throw new ArgumentException("Категория не найдена");

    category.Name = "Arcada";
    context.SaveChanges();
}

static void UpdateCategory(AppDbContext context)
{
    var category = new Category()
    {
        CategoryId = 1,
        Name = "arcada"
    };
    context.Categories.Update(category);
    context.SaveChanges();
}

static void Sort(StoreDbContext context)
{
    var games = context.Games
        .OrderBy(g => g.Price);
    foreach (var g in games)
        Console.WriteLine($"{g.Name} {g.Price}");
}
