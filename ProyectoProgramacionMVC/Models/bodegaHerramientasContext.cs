using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProyectoProgramacionMVC.Models
{
    public partial class bodegaHerramientasContext : DbContext
    {
        public bodegaHerramientasContext()
        {
        }

        public bodegaHerramientasContext(DbContextOptions<bodegaHerramientasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Herramientum> Herramienta { get; set; } = null!;
        public virtual DbSet<Mantencion> Mantencions { get; set; } = null!;
        public virtual DbSet<Marca> Marcas { get; set; } = null!;
        public virtual DbSet<Modelo> Modelos { get; set; } = null!;
        public virtual DbSet<Movimiento> Movimientos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<VwHerramientasEnUsoPorUsuario> VwHerramientasEnUsoPorUsuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=Jose\\SQLEXPRESS01; database=bodegaHerramientas; integrated security=true; TrustServerCertificate=Yes");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Herramientum>(entity =>
            {
                entity.HasKey(e => e.HerramientaId)
                    .HasName("PK__HERRAMIE__D30678DD9A6780CD");

                entity.ToTable("HERRAMIENTA");

                entity.HasIndex(e => e.Estado, "IX_Herramienta_Estado");

                entity.HasIndex(e => e.NumeroSerie, "UQ_NumeroSerie")
                    .IsUnique();

                entity.Property(e => e.HerramientaId).HasColumnName("herramienta_id");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModeloId).HasColumnName("modelo_id");

                entity.Property(e => e.NumeroSerie)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("numero_serie");

                entity.HasOne(d => d.Modelo)
                    .WithMany(p => p.Herramienta)
                    .HasForeignKey(d => d.ModeloId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Herramienta_Modelo");
            });

            modelBuilder.Entity<Mantencion>(entity =>
            {
                entity.ToTable("MANTENCION");

                entity.HasIndex(e => new { e.FechaInicio, e.FechaFin }, "IX_Mantencion_Fechas");

                entity.Property(e => e.MantencionId).HasColumnName("mantencion_id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_fin");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_inicio")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.HerramientaId).HasColumnName("herramienta_id");

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

                entity.HasOne(d => d.Herramienta)
                    .WithMany(p => p.Mantencions)
                    .HasForeignKey(d => d.HerramientaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mantencion_Herramienta");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Mantencions)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mantencion_Usuario");
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.ToTable("MARCA");

                entity.Property(e => e.MarcaId).HasColumnName("marca_id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Modelo>(entity =>
            {
                entity.ToTable("MODELO");

                entity.Property(e => e.ModeloId).HasColumnName("modelo_id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.MarcaId).HasColumnName("marca_id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.Marca)
                    .WithMany(p => p.Modelos)
                    .HasForeignKey(d => d.MarcaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Modelo_Marca");
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.ToTable("MOVIMIENTO");

                entity.HasIndex(e => e.Fecha, "IX_Movimiento_Fecha");

                entity.Property(e => e.MovimientoId).HasColumnName("movimiento_id");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.HerramientaId).HasColumnName("herramienta_id");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("observacion");

                entity.Property(e => e.TipoMovimiento)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tipo_movimiento");

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

                entity.HasOne(d => d.Herramienta)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.HerramientaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movimiento_Herramienta");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movimiento_Usuario");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("USUARIO");

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<VwHerramientasEnUsoPorUsuario>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_HerramientasEnUsoPorUsuario");

                entity.Property(e => e.HerramientasEnUso).HasColumnName("herramientas_en_uso");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
