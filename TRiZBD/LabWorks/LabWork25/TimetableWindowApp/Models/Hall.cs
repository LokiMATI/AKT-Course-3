using System;
using System.Collections.Generic;

namespace TimetableWindowApp.Models;

public partial class Hall
{
    public byte HallId { get; set; }

    public string Cinema { get; set; } = null!;

    public byte HallNumber { get; set; }

    public byte RowsQuantity { get; set; }

    public byte SeatsQuantity { get; set; }

    public bool IsVip { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
