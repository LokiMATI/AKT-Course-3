using System;
using System.Collections.Generic;

namespace ConsoleApp.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
