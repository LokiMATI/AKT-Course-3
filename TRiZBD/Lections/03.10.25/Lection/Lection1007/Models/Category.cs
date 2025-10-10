using System.ComponentModel.DataAnnotations.Schema;

namespace Lection1007.Models;

[Table("Category")]
public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }

    public IEnumerable<Game>? Games { get; set; }
}
