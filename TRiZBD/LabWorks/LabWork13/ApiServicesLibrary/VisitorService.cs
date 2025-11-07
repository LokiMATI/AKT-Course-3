using LabLibrary.Models;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Data;
using System.Net;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;

namespace ApiServicesLibrary;

public class VisitorService(HttpClient client)
{
    private readonly HttpClient _client = client;
    JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task<List<Visitor>> GetVisitorsAsync()
    {
        var response = await _client.GetAsync("Visitors");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new Exception("Ресурс не найден");
        }

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception();
        }

        if (response.StatusCode == HttpStatusCode.NoContent)
        {
            return new();
        }

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Visitor>>(content, _jsonOptions);
    }

    public async Task<Visitor?> GetVisitorAsync(int id)
    {
        var response = await _client.GetAsync($"Visitors/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        if (!response.IsSuccessStatusCode)
            throw new Exception("Неуспешный вызов.");

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Visitor>(content, _jsonOptions);
    }

    public async Task<Visitor> AddVisitorAsync(Visitor film)
    {
        var json = JsonSerializer.Serialize(film, _jsonOptions);
        var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("Visitors", requestContent);

        if (!response.IsSuccessStatusCode)
            throw new Exception();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Visitor>(responseContent, _jsonOptions);
    }

    public async Task<bool> EditVisitorAsync(Visitor Visitor)
    {
        var json = JsonSerializer.Serialize(Visitor, _jsonOptions);
        var requestContent = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PutAsync($"Visitors/{Visitor.VisitorId}", requestContent);

        bool status = response.StatusCode switch
        {
            HttpStatusCode.NotFound => false,
            _ when response.IsSuccessStatusCode => true,
            _ => throw new Exception()
        };

        return status;


    }

    public async Task DeleteVisitorAsync()
    {
        var response = await _client.DeleteAsync("Visitors");
        response.EnsureSuccessStatusCode();
    }
}
