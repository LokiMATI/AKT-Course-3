using ConsoleApp.DataAccessLayer;
using System.Threading.Channels;

Console.WriteLine($"Задание 1.1\n{DataAccessLayer.ConnectingString}\n");

//Console.WriteLine("Задание 1.2");
//Console.Write("Название сервера: ");
//string serverName = Console.ReadLine() ?? "";
//Console.Write("Название базы данных: ");
//string dataBase = Console.ReadLine() ?? "";
//Console.Write("Имя пользователя: ");
//string login = Console.ReadLine() ?? "";
//Console.Write("Пароль: ");
//string password = Console.ReadLine() ?? "";

//DataAccessLayer.ChangeConnectingString(serverName, dataBase, login, password);
//Console.WriteLine($"{DataAccessLayer.ConnectingString}\n");

//Console.WriteLine("Задание 1.3");
//Console.WriteLine(DataAccessLayer.CheckConnection() ? "Подключение успешно." : "Подключение провально.");

//Console.WriteLine("Задание 2.1");
//Console.Write("Запрос: ");
//string expression = Console.ReadLine() ?? "";
//Console.WriteLine($"Количество изменённых строк: {await DataAccessLayer.ExecuteNonQueryAsync(expression)}\n");

//Console.WriteLine("Задание 2.2");
//Console.Write("Запрос: ");
//string expression = Console.ReadLine() ?? "";
//Console.WriteLine($"Количество изменённых строк: {await DataAccessLayer.ExecuteScalarAsync(expression)}\n");

//Console.WriteLine("Задание 3");
//Console.Write("Номер сеанса: ");
//int sessionId = int.Parse(Console.ReadLine() ?? "1");
//Console.Write("Новая цена билета: ");
//double price = double.Parse(Console.ReadLine() ?? "100.00");
//Console.WriteLine(await DataAccessLayer.ChangeTicketPriceAsync(sessionId, price) > 0 ? "Изменения внесены.\n" : "Изменения отсутсвуют.\n");

Console.WriteLine("Задание 4");
Console.Write("Номер сеанса: ");
int sessionId = int.Parse(Console.ReadLine() ?? "1");
Console.Write("Новая цена билета: ");
double price = double.Parse(Console.ReadLine() ?? "100.00");
Console.WriteLine(await DataAccessLayer.ChangeTicketPriceAsync(sessionId, price) > 0 ? "Изменения внесены.\n" : "Изменения отсутсвуют.\n");


//Console.WriteLine("Задание 5");
//Console.Write("Номер фильма: ");
//int filmId = int.Parse(Console.ReadLine() ?? "1");
//Console.Write("Имя файла: ");
//string fileName = Console.ReadLine() ?? "1";
//Console.WriteLine(await DataAccessLayer.LoadPosterAsync(filmId, fileName));