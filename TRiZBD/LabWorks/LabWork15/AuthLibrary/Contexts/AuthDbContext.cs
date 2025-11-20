using AuthLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthLibrary.Contexts;

public partial class AuthDbContext : DbContext
{
    public AuthDbContext()
    {
    }

    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CinemaPrivilege> CinemaPrivileges { get; set; }

    public virtual DbSet<CinemaRolePrivilege> CinemaRolePrivileges { get; set; }

    public virtual DbSet<CinemaUser> CinemaUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=mssql;Initial Catalog=ispp3113;User ID=ispp3113;Password=3113;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CinemaPrivilege>(entity =>
        {
            entity.HasKey(e => e.PrivilegeId);

            entity.ToTable("CinemaPrivilege");

            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<CinemaRolePrivilege>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.PrivilegeId });

            entity.ToTable("CinemaRolePrivilege");

            entity.HasOne(d => d.Privilege).WithMany(p => p.CinemaRolePrivileges)
                .HasForeignKey(d => d.PrivilegeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CinemaRolePrivilege_CinemaPrivilege");
        });

        modelBuilder.Entity<CinemaUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("CinemaUser");

            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
