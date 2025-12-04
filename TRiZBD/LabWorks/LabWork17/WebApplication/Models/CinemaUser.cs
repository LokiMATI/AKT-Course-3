using System;
using System.Collections.Generic;

namespace WebApplication.Models;

public partial class CinemaUser
{
    public int UserId { get; set; }

    public string Login { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int UnsuccessfulAttemptsNumber { get; set; }

    public DateTime? UnblockingDate { get; set; }

    public int RoleId { get; set; }

    public virtual CinemaUserRole Role { get; set; } = null!;
}
