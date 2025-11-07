using Lection1107.Contexts;
using Lection1107.Models;


var login = "admin";
var password = "123";
using var context = new AppDbContext();
var user = context.Users.FirstOrDefault(u => u.Login == login);

if (user is null)
{
    Console.WriteLine("Not found");
    return;
}

if (IsUserLocked(user))
{
    Console.WriteLine($"Locked until {user.LockedUntil:HH:mm:ss}");
    return;
}

if (!IsCorrectPassword(password, user))
{
    Console.WriteLine("Incorrect passord");
    context.SaveChanges();
    return;
}

SuccessLogin(user);
context.SaveChanges();

Console.WriteLine("Welcome");
return;

static bool IsUserLocked(User user)
{
    if (user.LockedUntil.HasValue && user.LockedUntil <= DateTime.UtcNow)
    {
        user.FailedLoginAttempts = 0;
        user.LockedUntil = null;
        return false;
    }

    return user.LockedUntil.HasValue;
}

static bool IsCorrectPassword(string password, User user)
{
    int attempts = 3;
    int duration = 30;

    if (user.Password != password)
    {
        user.FailedLoginAttempts++;
        if (user.FailedLoginAttempts >= attempts)
            user.LockedUntil = DateTime.UtcNow.AddSeconds(duration);
        return false;
    }

    return true;
}

static void SuccessLogin(User user)
{
    user.FailedLoginAttempts = 0;
    //user.LockedUntil = null;
    user.LastAccess = DateTime.UtcNow;
}