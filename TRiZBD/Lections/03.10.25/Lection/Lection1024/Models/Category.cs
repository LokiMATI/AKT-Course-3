using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Lection1024.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
