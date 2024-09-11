using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionTecnica.Models;

public partial class EvaluacionTecnicaDbContext : DbContext
{
    public EvaluacionTecnicaDbContext()
    {
    }

    public EvaluacionTecnicaDbContext(DbContextOptions<EvaluacionTecnicaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=PC-ANGEL; Database=EvaluacionTecnicaDB; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07C065A86B");

            entity.HasIndex(e => e.Nombre, "Ix_Nombre_Roles").IsUnique();

            entity.Property(e => e.FechaTransaccion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Transaccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioTransaccion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValueSql("(user_name())")
                .HasColumnName("Usuario_Transaccion");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC075D8451D0");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Cedula, "Ix_Cedula_Usu").IsUnique();

            entity.HasIndex(e => e.FechaNacimiento, "Ix_Fecha_Nacimiento");

            entity.HasIndex(e => e.UsuarioNombre, "Ix_Usuario_Nombre").IsUnique();

            entity.Property(e => e.Apellido)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Contrasena)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Nacimiento");
            entity.Property(e => e.FechaTransaccion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Transaccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioNombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Usuario_Nombre");
            entity.Property(e => e.UsuarioTransaccion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValueSql("(user_name())")
                .HasColumnName("Usuario_Transaccion");

            entity.HasOne(d => d.Roles).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ROLESID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
