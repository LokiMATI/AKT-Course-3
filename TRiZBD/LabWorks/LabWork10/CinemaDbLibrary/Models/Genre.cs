using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaDbLibrary.Models;

[Table("Genre")]
public class Genre
{
    public int GenreId { get; set; }
    public string Title { get; set; } = null!;

    public List<Film> Films { get; set; } = new List<Film>();
}
