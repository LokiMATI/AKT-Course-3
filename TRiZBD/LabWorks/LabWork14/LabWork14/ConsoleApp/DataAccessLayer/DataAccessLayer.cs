using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ConsoleApp.DataAccessLayer;

static class DataAccessLayer
{
    private static string _serverName = "mssql";
    private static string _dataBase = "ispp3113";
    private static string _login = "ispp3113";
    private static string _password = "3113";

    public static string ConnectingString
    {
        get
        {
            SqlConnectionStringBuilder builder = new()
            {
                DataSource = _serverName,
                InitialCatalog = _dataBase,
                UserID = _login,
                Password = _password,
                TrustServerCertificate = true
            };

            return builder.ConnectionString;
        }
    }

    public static void ChangeConnectingString(
        string serverName,  
        string dataBase, 
        string login, 
        string password)
    {
        _serverName = serverName;
        _dataBase = dataBase;
        _login = login;
        _password = password;
    }

    public static bool CheckConnection()
    {
        using SqlConnection connection = new(ConnectingString);
        try
        {
            connection.Open();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static async Task<int> ExecuteNonQueryAsync(string expression)
    {
        using SqlConnection connection = new(ConnectingString);
        connection.Open();

        SqlCommand command = new(expression, connection);
        return await command.ExecuteNonQueryAsync();
    }

    public static async Task<object?> ExecuteScalarAsync(string expression)
    {
        using SqlConnection connection = new(ConnectingString);
        connection.Open();

        SqlCommand command = new(expression, connection);
        return await command.ExecuteScalarAsync();
    }

    public static async Task<int> ChangeTicketPriceAsync(int sessionId, double price)
    {
        using SqlConnection connection = new(ConnectingString);
        connection.Open();

        string expression = "UPDATE Session SET Price = @price WHERE SessionId = @sessionId";

        SqlCommand command = new(expression, connection);

        command.Parameters.AddWithValue("@price", price);
        command.Parameters.AddWithValue("@sessionId", sessionId);

        return await command.ExecuteNonQueryAsync();
    }

    public static async Task<int> LoadPosterAsync(int filmId, string fileName)
    {
        using SqlConnection connection = new(ConnectingString);
        connection.Open();

        byte[] fileData = File.ReadAllBytes(fileName);
        Console.WriteLine(fileData.Length);
        if (fileData.Length < 1024) // проверка, что размер меньше 1 КБ
        {
            string expression = "UPDATE Film SET Poster = @posterData WHERE FilmId = @filmId";
            SqlCommand command = new(expression, connection);
            command.Parameters.AddWithValue("@posterData", fileData);
            command.Parameters.AddWithValue("@filmId", filmId);

            return await command.ExecuteNonQueryAsync();
        }
        return -1;    
    }
}