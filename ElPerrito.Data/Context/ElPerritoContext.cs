using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

using ElPerrito.Data.Entities;
namespace ElPerrito.Data.Context;

public partial class ElPerritoContext : DbContext
{
    public ElPerritoContext()
    {
    }

    public ElPerritoContext(DbContextOptions<ElPerritoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrito> Carritos { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetalleCarrito> DetalleCarritos { get; set; }

    public virtual DbSet<DetalleVentum> DetalleVenta { get; set; }

    public virtual DbSet<Direccion> Direccions { get; set; }

    public virtual DbSet<DireccionEnvio> DireccionEnvios { get; set; }

    public virtual DbSet<Envio> Envios { get; set; }

    public virtual DbSet<HistorialUsuario> HistorialUsuarios { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoImagen> ProductoImagens { get; set; }

    public virtual DbSet<RegistroActividad> RegistroActividads { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;database=elperrito;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.IdCarrito).HasName("PRIMARY");

            entity.Property(e => e.Estado).HasDefaultValueSql("'activo'");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Carritos).HasConstraintName("fk_cart_cliente");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PRIMARY");

            entity.Property(e => e.Activa).HasDefaultValueSql("'1'");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PRIMARY");

            entity.Property(e => e.Estado).HasDefaultValueSql("'activo'");
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("current_timestamp()");
        });

        modelBuilder.Entity<DetalleCarrito>(entity =>
        {
            entity.HasKey(e => e.IdItem).HasName("PRIMARY");

            entity.HasOne(d => d.IdCarritoNavigation).WithMany(p => p.DetalleCarritos).HasConstraintName("fk_dcart_cart");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleCarritos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dcart_prod");
        });

        modelBuilder.Entity<DetalleVentum>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PRIMARY");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dventa_prod");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta).HasConstraintName("fk_dventa_venta");
        });

        modelBuilder.Entity<Direccion>(entity =>
        {
            entity.HasKey(e => e.IdDireccion).HasName("PRIMARY");

            entity.Property(e => e.Activa).HasDefaultValueSql("'1'");
            entity.Property(e => e.Alias).HasComment("Ej: Casa, Trabajo, Oficina");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Direccions).HasConstraintName("direccion_ibfk_1");
        });

        modelBuilder.Entity<DireccionEnvio>(entity =>
        {
            entity.HasKey(e => e.IdDireccion).HasName("PRIMARY");

            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.FechaModificacion).ValueGeneratedOnAddOrUpdate();

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.DireccionEnvios).HasConstraintName("fk_direccion_cliente");
        });

        modelBuilder.Entity<Envio>(entity =>
        {
            entity.HasKey(e => e.IdEnvio).HasName("PRIMARY");

            entity.Property(e => e.EstadoEnvio).HasDefaultValueSql("'pendiente'");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.Paqueteria).HasComment("DHL, FedEx, Estafeta, etc.");
            entity.Property(e => e.ReceptorFirma).HasComment("URL de imagen de firma");

            entity.HasOne(d => d.IdDireccionNavigation).WithMany(p => p.Envios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("envio_ibfk_2");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Envios).HasConstraintName("envio_ibfk_1");
        });

        modelBuilder.Entity<HistorialUsuario>(entity =>
        {
            entity.HasKey(e => e.IdHistorial).HasName("PRIMARY");

            entity.Property(e => e.FechaCambio).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.HistorialUsuarioIdUsuarioNavigations).HasConstraintName("fk_historial_usuario");

            entity.HasOne(d => d.IdUsuarioModificadorNavigation).WithMany(p => p.HistorialUsuarioIdUsuarioModificadorNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_historial_modificador");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PRIMARY");

            entity.Property(e => e.IdProducto).ValueGeneratedNever();

            entity.HasOne(d => d.IdProductoNavigation).WithOne(p => p.Inventario).HasConstraintName("fk_inv_prod");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PRIMARY");

            entity.Property(e => e.Estado).HasDefaultValueSql("'pendiente'");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.Referencia).HasComment("ID de transacción o referencia bancaria");
            entity.Property(e => e.UltimosDigitos).HasComment("Últimos 4 dígitos de tarjeta");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Pagos).HasConstraintName("pago_ibfk_1");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PRIMARY");

            entity.Property(e => e.Activo).HasDefaultValueSql("'1'");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_prod_cat");

            entity.HasOne(d => d.IdUsuarioCreadorNavigation).WithMany(p => p.Productos).HasConstraintName("fk_producto_usuario");
        });

        modelBuilder.Entity<ProductoImagen>(entity =>
        {
            entity.HasKey(e => e.IdImagen).HasName("PRIMARY");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductoImagens).HasConstraintName("fk_pimg_producto");
        });

        modelBuilder.Entity<RegistroActividad>(entity =>
        {
            entity.HasKey(e => e.IdRegistro).HasName("PRIMARY");

            entity.Property(e => e.FechaAccion).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.RegistroActividads).HasConstraintName("fk_registro_usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.Property(e => e.Activo).HasDefaultValueSql("'1'");
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.Rol).HasDefaultValueSql("'admin'");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PRIMARY");

            entity.Property(e => e.EstadoEnvio).HasDefaultValueSql("'pendiente'");
            entity.Property(e => e.EstadoPago).HasDefaultValueSql("'pendiente'");
            entity.Property(e => e.Fecha).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_venta_cliente");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
