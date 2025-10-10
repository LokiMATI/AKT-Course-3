using Microsoft.Data.SqlClient;
using System.Data;

Console.WriteLine("Разработка клиента");

var connectionString = "..."; // или ConnectionStringBuilder
using IDbConnection connection = new SqlConnection(connectionString);
string query = "INSERT INTO Category(Title) OUTPUT INSERTED.CategoryId VALUES(@CategoryId)";

connection.          //Чел, закрывай, сохраняй лекции и выходи из приложений