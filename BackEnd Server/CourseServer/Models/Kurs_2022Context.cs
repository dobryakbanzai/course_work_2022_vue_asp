using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CourseServer
{
    public partial class Kurs_2022Context : DbContext
    {
        public Kurs_2022Context()
        {
        }

        public Kurs_2022Context(DbContextOptions<Kurs_2022Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Challenges> Challenges { get; set; }
        public virtual DbSet<Classes> Classes { get; set; }
        public virtual DbSet<StudCh> StudCh { get; set; }
        public virtual DbSet<StudTeach> StudTeach { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Teachers> Teachers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Kurs_2022;Username=postgres;Password=200018");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Challenges>(entity =>
            {
                entity.ToTable("challenges");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChName)
                    .IsRequired()
                    .HasColumnName("ch_name")
                    .HasMaxLength(50);

                entity.Property(e => e.ChScore).HasColumnName("ch_score");
            });

            modelBuilder.Entity<Classes>(entity =>
            {
                entity.ToTable("classes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClassNum).HasColumnName("class_num");
            });

            modelBuilder.Entity<StudCh>(entity =>
            {
                entity.ToTable("stud_ch");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChId).HasColumnName("ch_id");

                entity.Property(e => e.StudChScore).HasColumnName("stud_ch_score");

                entity.Property(e => e.StudId).HasColumnName("stud_id");

                entity.HasOne(d => d.Ch)
                    .WithMany(p => p.StudCh)
                    .HasForeignKey(d => d.ChId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("stud_ch_ch_id_fkey");

                entity.HasOne(d => d.Stud)
                    .WithMany(p => p.StudCh)
                    .HasForeignKey(d => d.StudId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("stud_ch_stud_id_fkey");
            });

            modelBuilder.Entity<StudTeach>(entity =>
            {
                entity.ToTable("stud_teach");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.StudId).HasColumnName("stud_id");

                entity.Property(e => e.TeachId).HasColumnName("teach_id");

                entity.HasOne(d => d.Stud)
                    .WithMany(p => p.StudTeach)
                    .HasForeignKey(d => d.StudId)
                    .HasConstraintName("stud_teach_stud_id_fkey");

                entity.HasOne(d => d.Teach)
                    .WithMany(p => p.StudTeach)
                    .HasForeignKey(d => d.TeachId)
                    .HasConstraintName("stud_teach_teach_id_fkey");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.ToTable("students");

                entity.HasIndex(e => e.Login)
                    .HasName("students_login_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.FName)
                    .IsRequired()
                    .HasColumnName("f_name")
                    .HasMaxLength(50);

                entity.Property(e => e.LName)
                    .IsRequired()
                    .HasColumnName("l_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(50);

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasColumnName("pass")
                    .HasMaxLength(50);

                entity.Property(e => e.TotalScore).HasColumnName("total_score");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("students_class_id_fkey");
            });

            modelBuilder.Entity<Teachers>(entity =>
            {
                entity.ToTable("teachers");

                entity.HasIndex(e => e.Login)
                    .HasName("teachers_login_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FName)
                    .IsRequired()
                    .HasColumnName("f_name")
                    .HasMaxLength(50);

                entity.Property(e => e.LName)
                    .IsRequired()
                    .HasColumnName("l_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(50);

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasColumnName("pass")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
