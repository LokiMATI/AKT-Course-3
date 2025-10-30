using Lection1024.Models;

namespace Lection1024.DTOs;

public class GameDto
{
    public int Id { get; set; }

    public string Category { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

}

public static class GameExtensions
{
    public static GameDto ToDto(this Game game)
        => new()
        {
            Id = game.GameId,
            Name = game.Name,
            Category = game.Category.Name,
            Price = game.Price
        };
}