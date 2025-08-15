using System;
using System.Collections.Generic;
using Api_FiesteDocs.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_FiesteDocs.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ensayo> Ensayos { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Grupo> Grupos { get; set; }

    public virtual DbSet<Instrumento> Instrumentos { get; set; }

    public virtual DbSet<Partitura> Partituras { get; set; }

    public virtual DbSet<Seccion> Seccions { get; set; }

    public virtual DbSet<Trabajo> Trabajos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ensayo>(entity =>
        {
            entity.HasKey(e => e.IdEnsayo);

            entity.ToTable("ensayo");

            entity.Property(e => e.IdEnsayo).HasColumnName("id_ensayo");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.HoraFin)
                .HasPrecision(0)
                .HasColumnName("hora_fin");
            entity.Property(e => e.HoraInicio)
                .HasPrecision(0)
                .HasColumnName("hora_inicio");
            entity.Property(e => e.IdGrupo).HasColumnName("id_grupo");

            entity.HasOne(d => d.IdGrupoNavigation).WithMany(p => p.Ensayos)
                .HasForeignKey(d => d.IdGrupo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ensayo_grupo");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante);

            entity.ToTable("estudiante");

            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.Documento)
                .HasMaxLength(12)
                .HasColumnName("documento");
            entity.Property(e => e.IdGrupo).HasColumnName("id_grupo");
            entity.Property(e => e.IdInstrumento).HasColumnName("id_instrumento");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(4)
                .HasColumnName("tipo_documento");

            entity.HasOne(d => d.IdGrupoNavigation).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.IdGrupo)
                .HasConstraintName("FK_estudiante_grupo1");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_estudiante_usuario");
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.IdGrupo);

            entity.ToTable("grupo");

            entity.Property(e => e.IdGrupo).HasColumnName("id_grupo");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(30)
                .HasColumnName("ciudad");
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .HasColumnName("codigo");
            entity.Property(e => e.IdUsuarioDirector).HasColumnName("id_usuario_director");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Instrumento>(entity =>
        {
            entity.HasKey(e => e.IdInstrumento);

            entity.ToTable("instrumento");

            entity.Property(e => e.IdInstrumento)
                .ValueGeneratedNever()
                .HasColumnName("id_instrumento");
            entity.Property(e => e.IdSeccion)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_seccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdSeccionNavigation).WithMany(p => p.Instrumentos)
                .HasForeignKey(d => d.IdSeccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_instrumento_seccion");
        });

        modelBuilder.Entity<Partitura>(entity =>
        {
            entity.HasKey(e => e.IdPartitura);

            entity.ToTable("partitura");

            entity.Property(e => e.IdPartitura).HasColumnName("id_partitura");
            entity.Property(e => e.Archivo).HasColumnName("archivo");
            entity.Property(e => e.Comentarios)
                .HasMaxLength(100)
                .HasColumnName("comentarios");
            entity.Property(e => e.IdSeccion).HasColumnName("id_seccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdSeccionNavigation).WithMany(p => p.Partituras)
                .HasForeignKey(d => d.IdSeccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_partitura_seccion");
        });

        modelBuilder.Entity<Seccion>(entity =>
        {
            entity.HasKey(e => e.IdSeccion);

            entity.ToTable("seccion");

            entity.Property(e => e.IdSeccion).HasColumnName("id_seccion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Trabajo>(entity =>
        {
            entity.HasKey(e => e.IdTrabajo);

            entity.ToTable("trabajo");

            entity.Property(e => e.IdTrabajo).HasColumnName("id_trabajo");
            entity.Property(e => e.Comentarios)
                .HasMaxLength(300)
                .HasColumnName("comentarios");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.Detalles)
                .HasMaxLength(500)
                .HasColumnName("detalles");
            entity.Property(e => e.Evidencia).HasColumnName("evidencia");
            entity.Property(e => e.Foto).HasColumnName("foto");
            entity.Property(e => e.IdEnsayo).HasColumnName("id_ensayo");
            entity.Property(e => e.IdSeccion).HasColumnName("id_seccion");

            entity.HasOne(d => d.IdEnsayoNavigation).WithMany(p => p.Trabajos)
                .HasForeignKey(d => d.IdEnsayo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_trabajo_ensayo");

            entity.HasOne(d => d.IdSeccionNavigation).WithMany(p => p.Trabajos)
                .HasForeignKey(d => d.IdSeccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_trabajo_seccion");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(20)
                .HasColumnName("ciudad");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(150)
                .HasColumnName("contrasena");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .HasColumnName("correo");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Foto).HasColumnName("foto");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
