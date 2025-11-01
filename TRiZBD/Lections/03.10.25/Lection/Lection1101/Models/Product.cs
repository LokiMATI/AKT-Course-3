namespace Lection1101.Models;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public Category Category { get; set; }
    public string[] Images { get; set; }
    public DateTime CreationAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

