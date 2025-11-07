using LabLibrary.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ApiServicesLibrary;

public class GenreService(HttpClient client)
{
    private readonly HttpClient _client = client;
    JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task<List<Genre>> GetGenresAsync()
    {
        var response = await _client.GetAsync("Genres");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Genre>>(content, _jsonOptions) ?? new();
    }

    public async Task<Genre?> GetGenreAsync(int id)
    {
        var response = await _client.GetAsync($"Genres/{id}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Genre>(content, _jsonOptions);
    }

    public async Task<Genre> AddGenreAsync(Genre film)
    {
        var json = JsonSerializer.Serialize(film, _jsonOptions);
        var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("Genres", requestContent);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Genre>(responseContent, _jsonOptions);
    }

    public async Task EditGenreAsync(Genre Genre)
    {
        var json = JsonSerializer.Serialize(Genre, _jsonOptions);
        var requestContent = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PutAsync($"Genres/{Genre.GenreId}", requestContent);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteGenreAsync(int id)
    {
        var response = await _client.DeleteAsync($"Genres/{id}");
        response.EnsureSuccessStatusCode();
    }
}
