using System;
using System.Collections.Generic;

namespace ConsoleApp.Models;

public partial class Film
{
    internal static Task<IEnumerable<Genre>> genres;

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

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
