using LabWork9.Models;
using Microsoft.EntityFrameworkCore;

namespace LabWork9.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Visitor> Visitors { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=mssql;Initial Catalog=ispp3113;User ID=ispp3113;Password=3113;Trust Server Certificate=True");
    }
}
