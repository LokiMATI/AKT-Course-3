using Lection1101.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

Console.WriteLine("Call web-api");

var client = new HttpClient();
string baseUrl = "https://api.escuelajs.co/api/v1/";
client.BaseAddress = new(baseUrl);



//var category = new Category { };
//var json = JsonSerializer.Serialize(объект, jsonOptions);
//var content = new StringContent(json, Encoding.UTF8, "application/json");

//var response = await _client.МетодAsync(…, content);
//response.EnsureSuccessStatusCode();

//// получение объекта из ответа
//if (response.IsSuccessStatusCode)
//    var responseJson = await response.Content.ReadAsStringAsync();

//using var response = await client.GetAsync("categories");
//response.EnsureSuccessStatusCode();

//var jsonOptions = new JsonSerializerOptions
//{
//    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // с маленькой буквы названия
//    WriteIndented = true, // для отступов
//};

//var content = await response.Content.ReadAsStringAsync();
//var categories = JsonSerializer.Deserialize<List<Category>>(content, jsonOptions);




static async Task<(HttpResponseMessage responsePost, HttpResponseMessage responsePut, HttpResponseMessage responseDelete)> Check(HttpClient client)
{
    var categories = await client.GetFromJsonAsync<IEnumerable<Category>>("categories");

    int id = 41;
    var category = await client.GetFromJsonAsync<Category>($"categories/{id}");

    category = new()
    {
        Name = "testname",
        Image = "https://placeimg.com/640/480/any"
    };
    using var responsePost = await client.PostAsJsonAsync("categories", category);
    responsePost.EnsureSuccessStatusCode();

    var result = await responsePost.Content.ReadFromJsonAsync<Category>();

    category.Name = "newName";
    using var responsePut = await client.PutAsJsonAsync($"categories/{category.Id}", category);
    responsePut.EnsureSuccessStatusCode();

    using var responseDelete = await client.DeleteAsync($"categories/{category.Id}");
    responseDelete.EnsureSuccessStatusCode();
    return (responsePost, responsePut, responseDelete);
}