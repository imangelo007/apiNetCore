using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication1.Models
{
    public partial class pruebaContext : DbContext
    {
        public pruebaContext()
        {
        }

        public pruebaContext(DbContextOptions<pruebaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cursos> Cursos { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cursos>(entity =>
            {
                entity.ToTable("cursos");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

                entity.Property(e => e.Nombre)
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Cursos)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cursos_usuario");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .IsUnicode(false);
            });
        }
    }
}
