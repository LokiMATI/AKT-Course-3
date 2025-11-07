using LabLibrary.Models;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace ApiServicesLibrary;

public class FilmService(HttpClient client)
{
    private readonly HttpClient _client = client;

    public async Task<List<Film>> GetFilmsAsync() 
        => await _client.GetFromJsonAsync<List<Film>>("Films") ?? new();

    public async Task<Film> GetFilmAsync(int id)
        => await _client.GetFromJsonAsync<Film>($"Films/{id}");

    public async Task<Film> AddFilmAsync(Film film)
    {
        var response = await _client.PostAsJsonAsync("Films", film);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Film>();
    }

    public async Task EditFilmAsync(Film film)
    {
        var response = await _client.PutAsJsonAsync($"Films/{film.FilmId}", film);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteFilmAsync(int id)
    {
        var response = await _client.DeleteAsync($"Films/{id}");
        response.EnsureSuccessStatusCode();
    }
}
