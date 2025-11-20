namespace AuthLibrary.Services;

public class AuthService
{
    private string HashPassword(string password) 
        => BCrypt.Net.BCrypt.EnhancedHashPassword(password, 14);

    public 
}
