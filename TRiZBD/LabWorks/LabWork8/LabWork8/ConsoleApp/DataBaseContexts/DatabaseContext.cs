using Microsoft.Data.SqlClient;
using System.Data;

namespace ConsoleApp.DataBaseContexts;

public class DatabaseContext
{
    private readonly string _connectingString;

    public DatabaseContext(string server, string database, string login, string password)
    {
        SqlConnectionStringBuilder builder = new()
        {
            DataSource = server,
            InitialCatalog = database,
            UserID = login,
            Password = password,
            TrustServerCertificate = true
        };

        _connectingString = builder.ToString();
    }

    public IDbConnection CreateConnection() => new SqlConnection(_connectingString);
}
