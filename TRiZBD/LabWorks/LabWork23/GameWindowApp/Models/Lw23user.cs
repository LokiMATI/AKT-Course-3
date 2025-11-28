using System;
using System.Collections.Generic;

namespace GameWindowApp.Models;

public partial class Lw23user
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Passoword { get; set; } = null!;

    public string Ip { get; set; } = null!;

    public DateTime LastEnter { get; set; }
}
