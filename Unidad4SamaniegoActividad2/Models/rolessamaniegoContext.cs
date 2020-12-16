using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Unidad4SamaniegoActividad2.Models
{
    public partial class rolessamaniegoContext : DbContext
    {
        public rolessamaniegoContext()
        {
        }

        public rolessamaniegoContext(DbContextOptions<rolessamaniegoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alumno> Alumno { get; set; }
        public virtual DbSet<Director> Director { get; set; }
        public virtual DbSet<Docente> Docente { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;user=root;password=root;database=rolessamaniego", x => x.ServerVersion("8.0.17-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.ToTable("alumno");

                entity.HasIndex(e => e.IdDocente)
                    .HasName("fk_IdDocente");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.IdDocente).HasColumnType("int(11)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NumeroDeControl)
                    .IsRequired()
                    .HasColumnType("varchar(8)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdDocenteNavigation)
                    .WithMany(p => p.Alumno)
                    .HasForeignKey(d => d.IdDocente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_IdDocente");
            });

            modelBuilder.Entity<Director>(entity =>
            {
                entity.ToTable("director");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Clave).HasColumnType("int(11)");

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasComment("root")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Docente>(entity =>
            {
                entity.ToTable("docente");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Activo)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.Clave).HasColumnType("int(11)");

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
