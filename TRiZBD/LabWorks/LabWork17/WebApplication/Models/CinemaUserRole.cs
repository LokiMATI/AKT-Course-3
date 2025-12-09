using System;
using System.Collections.Generic;

namespace WebApplication.Models;

public partial class CinemaUserRole
{
    public int RoleId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<CinemaUser>? CinemaUsers { get; set; } = new List<CinemaUser>();
}
