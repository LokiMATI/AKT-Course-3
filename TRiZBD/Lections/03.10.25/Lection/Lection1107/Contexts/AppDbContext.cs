using Lection1107.Models;
using Microsoft.EntityFrameworkCore;

namespace Lection1107.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = app.db");
    }
}
