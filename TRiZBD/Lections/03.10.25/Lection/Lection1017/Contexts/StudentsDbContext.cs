using Lection1017.Models;
using Microsoft.EntityFrameworkCore;

namespace Lection1017.Contexts;

public class StudentsDbContext : DbContext
{
    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public StudentsDbContext()
    {
    }

    public StudentsDbContext(DbContextOptions<StudentsDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=mssql;Initial Catalog=ispp3113;User ID=ispp3113;Password=3113;Encrypt=True;Trust Server Certificate=True");
    }
}
