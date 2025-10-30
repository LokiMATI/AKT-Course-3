using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LabLibrary.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string Title { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
