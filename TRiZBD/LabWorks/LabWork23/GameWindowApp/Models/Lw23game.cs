using System;
using System.Collections.Generic;

namespace GameWindowApp.Models;

public partial class Lw23game
{
    public int IdGame { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int IdCategory { get; set; }

    public decimal Price { get; set; }

    public string? LogoFile { get; set; }

    public virtual ICollection<Lw23screenshot> Lw23screenshots { get; set; } = new List<Lw23screenshot>();
}
