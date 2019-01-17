using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication1.Models
{
    public partial class abcContext : DbContext
    {
        public abcContext()
        {
        }

        public abcContext(DbContextOptions<abcContext> options): base(options)
        {
        }

        public virtual DbSet<A> A { get; set; }
        public virtual DbSet<Ab> Ab { get; set; }
        public virtual DbSet<B> B { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<A>(entity =>
            {
                entity.ToTable("a");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Ab>(entity =>
            {
                entity.ToTable("ab");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.IdaNavigation)
                    .WithMany(p => p.Ab)
                    .HasForeignKey(d => d.Ida)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ab_a");

                entity.HasOne(d => d.IdbNavigation)
                    .WithMany(p => p.Ab)
                    .HasForeignKey(d => d.Idb)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ab_b");
            });

            modelBuilder.Entity<B>(entity =>
            {
                entity.ToTable("b");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(50);
            });
        }
    }
}
