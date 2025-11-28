using System;
using System.Collections.Generic;
using GameWindowApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GameWindowApp.Contexts;

public partial class GameDbContext : DbContext
{
    public GameDbContext()
    {
    }

    public GameDbContext(DbContextOptions<GameDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Lw23game> Lw23games { get; set; }

    public virtual DbSet<Lw23screenshot> Lw23screenshots { get; set; }

    public virtual DbSet<Lw23user> Lw23users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=mssql;Initial Catalog=ispp3113;User ID=ispp3113;Password=3113;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lw23game>(entity =>
        {
            entity.HasKey(e => e.IdGame).HasName("PK__LW23Game__A304AD9BFBAF71AB");

            entity.ToTable("LW23Games");

            entity.Property(e => e.IdGame).HasColumnName("idGame");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.IdCategory).HasColumnName("idCategory");
            entity.Property(e => e.LogoFile)
                .HasMaxLength(120)
                .HasColumnName("logoFile");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
        });

        modelBuilder.Entity<Lw23screenshot>(entity =>
        {
            entity.HasKey(e => e.ScreenshotId).HasName("PK__LW23Scre__DFD702B830ADA163");

            entity.ToTable("LW23Screenshots");

            entity.Property(e => e.FileName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Game).WithMany(p => p.Lw23screenshots)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LW23Scree__GameI__0B5CAFEA");
        });

        modelBuilder.Entity<Lw23user>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("LW23Users");

            entity.Property(e => e.Ip)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.Login)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Passoword).HasMaxLength(100);
            entity.Property(e => e.UserId).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
