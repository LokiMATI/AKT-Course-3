namespace Lection1107.Models;

public class User
{
    public int Id { get; set; }

    public required string Login { get; set; }

    public required string Password { get; set; }

    public int FailedLoginAttempts { get; set; } = 0;

    public DateTime? LockedUntil { get; set; } = null;

    public DateTime? LastAccess { get; set; } = null;
}
