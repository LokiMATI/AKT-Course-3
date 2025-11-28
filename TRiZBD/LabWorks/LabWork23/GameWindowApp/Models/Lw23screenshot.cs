using System;
using System.Collections.Generic;

namespace GameWindowApp.Models;

public partial class Lw23screenshot
{
    public int ScreenshotId { get; set; }

    public string FileName { get; set; } = null!;

    public byte[]? Photo { get; set; }

    public int GameId { get; set; }

    public virtual Lw23game Game { get; set; } = null!;
}
