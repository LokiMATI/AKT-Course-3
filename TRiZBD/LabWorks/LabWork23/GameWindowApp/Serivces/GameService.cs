using GameWindowApp.Contexts;
using GameWindowApp.Models;
using Microsoft.Win32;
using System.IO;
using System.Runtime.CompilerServices;

namespace GameWindowApp.Serivces;

public class GameService(GameDbContext context)
{
    private readonly GameDbContext _context = context;

    public async Task<Lw23game> AddLogoAsync(string name, string logoName)
    {
        var game = _context.Lw23games.FirstOrDefault(g => g.Name == name);

        if (game is null)
            throw new Exception("Игра НЕ надена.");

        game.LogoFile = logoName;
        SaveLogo(logoName);

        await _context.SaveChangesAsync();
        return game;
    }

    public async Task SaveScreenshotAsync(string name)
    {
        OpenFileDialog dialog = new();

        var game = _context.Lw23games.FirstOrDefault(g => g.Name == name);

        if (game is null)
            throw new Exception("Игра НЕ надена.");

        if (dialog.ShowDialog() != true)
            return;

        FileInfo logo = new(dialog.FileName);
        var bytes = LogoToByte(logo.FullName);

        Lw23screenshot screenshot = new()
        {
            FileName = logo.Name,
            GameId = game.IdGame,
            Photo = bytes
        };

        await _context.Lw23screenshots.AddAsync(screenshot);
        await _context.SaveChangesAsync();
    }

    private void SaveLogo(string logoPath)
    {
        FileInfo logo = new(logoPath);
        File.Copy(logoPath, $"./GamesLogo/{logo.Name}", true);
    }

    private byte[] LogoToByte(string filePath)
    {
        byte[] fileBytes = File.ReadAllBytes(filePath);

        if (fileBytes.Length > 2 * 1024 * 1024)
            throw new Exception("Размер файла не должен привышать 2 МБ.");

        return fileBytes;

    }
}
