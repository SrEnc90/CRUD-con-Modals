using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NeoCrudModal.Entidades;

namespace NeoCrudModal.Context;

public partial class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<CodigosPostale> CodigosPostales { get; set; }

    public virtual DbSet<Direccione> Direcciones { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<Ocasione> Ocasiones { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoOcasione> ProductoOcasiones { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Sucursale> Sucursales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPadreNavigation).WithMany(p => p.InverseIdPadreNavigation)
                .HasForeignKey(d => d.IdPadre)
                .HasConstraintName("FK_Categorias_Categorias");
        });

        modelBuilder.Entity<CodigosPostale>(entity =>
        {
            entity.Property(e => e.Asentamiento)
                .HasMaxLength(70)
                .IsUnicode(false);

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.CodigosPostales)
                .HasForeignKey(d => d.IdMunicipio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CodigosPostales_Municipios");
        });

        modelBuilder.Entity<Direccione>(entity =>
        {
            entity.Property(e => e.Calle)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Numero)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAsentamientoNavigation).WithMany(p => p.Direcciones)
                .HasForeignKey(d => d.IdAsentamiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Direcciones_CodigosPostales");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Estado1)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("Estado");
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.Property(e => e.Municipio1)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("Municipio");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Municipios)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Municipios_Estados");
        });

        modelBuilder.Entity<Ocasione>(entity =>
        {
            entity.Property(e => e.Ocasion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK_Productos_Categorias");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK_Productos_Proveedores");
        });

        modelBuilder.Entity<ProductoOcasione>(entity =>
        {
            entity.HasOne(d => d.IdOcasionNavigation).WithMany(p => p.ProductoOcasiones)
                .HasForeignKey(d => d.IdOcasion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductoOcasiones_Ocasiones");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductoOcasiones)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductoOcasiones_Productos");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.Property(e => e.Proveedor)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDireccionNavigation).WithMany(p => p.Proveedores)
                .HasForeignKey(d => d.IdDireccion)
                .HasConstraintName("FK_Proveedores_Direcciones");
        });

        modelBuilder.Entity<Sucursale>(entity =>
        {
            entity.Property(e => e.Sucursal)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDireccionesNavigation).WithMany(p => p.Sucursales)
                .HasForeignKey(d => d.IdDirecciones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sucursales_Direcciones");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
