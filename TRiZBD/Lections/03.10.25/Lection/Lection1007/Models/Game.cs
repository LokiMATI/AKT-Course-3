using System.ComponentModel.DataAnnotations.Schema;

namespace Lection1007.Models;

[Table("Game")]
public class Game
{
    public int GameId { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string KeysAmount { get; set; }

    public Category? Category { get; set; }
}
