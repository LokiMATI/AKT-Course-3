using GameWindowApp.Contexts;
using GameWindowApp.Models;
using Microsoft.Win32;
using System.IO;

namespace GameWindowApp.Serivces;

public class UserService(GameDbContext context)
{
    private readonly GameDbContext _context = context;

    public async Task ImportUsersAsync()
    {
        OpenFileDialog dialog = new();

        if (dialog.ShowDialog() != true)
            return;

        string[] lines = await File.ReadAllLinesAsync(dialog.FileName);
        Lw23user user;

        foreach (string line in lines)
        {
            string[] fields = line.Split(',');

            await _context.Lw23users.AddAsync(new()
            {
                UserId = int.Parse(fields[0]),
                Name = fields[1],
                Login = fields[2],
                Passoword = fields[3],
                Ip = fields[4],
                LastEnter = 

            });
        }
    }
}

