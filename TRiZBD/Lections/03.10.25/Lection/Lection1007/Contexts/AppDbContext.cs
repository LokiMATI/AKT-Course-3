using Lection1007.Models;
using Microsoft.EntityFrameworkCore;

namespace Lection1007.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Game> Games => Set<Game>();

    public AppDbContext()
    {
        //Database.EnsureDeleted();
        //Database.EnsureCreated();
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlite("Data Source = test.db");
        optionsBuilder.UseSqlServer("Data Source=mssql;Initial Catalog=ispp3113;User ID=ispp3113;Password=3113;Trust Server Certificate=True");
    }
}
