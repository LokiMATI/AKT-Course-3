using Lection1101.Models;
using System.Net.Http.Json;

Console.WriteLine("Call web-api");

var client = new HttpClient();
string baseUrl = "https://api.escuelajs.co/api/v1/";
client.BaseAddress = new(baseUrl);

var categories = await client.GetFromJsonAsync<IEnumerable<Category>>("categories");

int id = 41;
var category = await client.GetFromJsonAsync<Category>($"categories/{id}");

category = new()
{
    Name = "testname",
    Image = "i.png"
};

using var response = await client.PostAsJsonAsync("categories", category);
response.EnsureSuccessStatusCode();