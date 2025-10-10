using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaDbLibrary.Models;

[Table("Film")]
public class Film
{
    public int FilmId { get; set; }
    public string Title { get; set; } = null!;
    public short Duration { get; set; }
    public short Year { get; set; }
    public string? Description { get; set; }
    public byte[]? Poster { get; set; }
    public string? AgeRating { get; set; }
    public DateOnly? RentalStart { get; set; }
    public DateOnly? RentalEnd { get; set; }
    public bool IsDeleted { get; set; }

    public List<Genre> Genres { get; set; } = new List<Genre>();
}
