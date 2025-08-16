using System;
using System.Collections.Generic;
using Api_FiesteDocs.Entities;
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
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante);

            entity.ToTable("estudiante");

            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.Documento)
                .IsRequired()
                .HasMaxLength(12)
                .HasColumnName("documento");
            entity.Property(e => e.IdGrupo).HasColumnName("id_grupo");
            entity.Property(e => e.IdInstrumento).HasColumnName("id_instrumento");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.TipoDocumento)
                .IsRequired()
                .HasMaxLength(4)
                .HasColumnName("tipo_documento");
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.IdGrupo);

            entity.ToTable("grupo");

            entity.Property(e => e.IdGrupo).HasColumnName("id_grupo");
            entity.Property(e => e.Ciudad)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("ciudad");
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .HasColumnName("codigo");
            entity.Property(e => e.IdUsuarioDirector).HasColumnName("id_usuario_director");
            entity.Property(e => e.Nombre)
                .IsRequired()
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
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Partitura>(entity =>
        {
            entity.HasKey(e => e.IdPartitura);

            entity.ToTable("partitura");

            entity.Property(e => e.IdPartitura).HasColumnName("id_partitura");
            entity.Property(e => e.Archivo)
                .IsRequired()
                .HasColumnName("archivo");
            entity.Property(e => e.Comentarios)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("comentarios");
            entity.Property(e => e.IdSeccion).HasColumnName("id_seccion");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Seccion>(entity =>
        {
            entity.HasKey(e => e.IdSeccion);

            entity.ToTable("seccion");

            entity.Property(e => e.IdSeccion).HasColumnName("id_seccion");
            entity.Property(e => e.Descripcion)
                .IsRequired()
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
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.Detalles)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("detalles");
            entity.Property(e => e.Evidencia).HasColumnName("evidencia");
            entity.Property(e => e.Foto).HasColumnName("foto");
            entity.Property(e => e.IdEnsayo).HasColumnName("id_ensayo");
            entity.Property(e => e.IdSeccion).HasColumnName("id_seccion");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Ciudad)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("ciudad");
            entity.Property(e => e.Contrasena)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("contrasena");
            entity.Property(e => e.Correo)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("correo");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Foto)
                .IsRequired()
                .HasColumnName("foto");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
