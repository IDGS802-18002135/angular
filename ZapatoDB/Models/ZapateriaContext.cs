using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ZapatoDB.Models;

public partial class ZapateriaContext : DbContext
{
    public ZapateriaContext()
    {
    }

    public ZapateriaContext(DbContextOptions<ZapateriaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Zapato> Zapatos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Zapato>(entity =>
        {
            entity.HasKey(e => e.IdZapato).HasName("PK__zapato__CE3CAAB6D062ACA2");

            entity.ToTable("zapato");

            entity.Property(e => e.IdZapato).HasColumnName("idZapato");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Imagen)
                .HasColumnType("text")
                .HasColumnName("imagen");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio).HasColumnName("precio");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
