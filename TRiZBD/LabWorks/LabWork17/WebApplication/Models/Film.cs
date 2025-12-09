using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models;

public partial class Film
{
    public int FilmId { get; set; }

    public string Title { get; set; } = null!;

    [DataType(DataType.MultilineText)]
    public short Duration { get; set; }

    public short Year { get; set; }

    public string? Description { get; set; }

    public byte[]? Poster { get; set; }

    public string? AgeRating { get; set; }

    public DateOnly? RentalStart { get; set; }

    public DateOnly? RentalEnd { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Session>? Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Genre>? Genres { get; set; } = new List<Genre>();
}
