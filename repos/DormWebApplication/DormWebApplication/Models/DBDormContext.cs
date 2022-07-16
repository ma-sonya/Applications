using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DormWebApplication
{
    public partial class DBDormContext : DbContext
    {
        public DBDormContext()
        {
        }

        public DBDormContext(DbContextOptions<DBDormContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Duty> Duties { get; set; } = null!;
        public virtual DbSet<Faculty> Faculties { get; set; } = null!;
        public virtual DbSet<Floor> Floors { get; set; } = null!;
        public virtual DbSet<Furniture> Furnitures { get; set; } = null!;
        public virtual DbSet<Inhabitant> Inhabitants { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server = LAPTOP-987VEQHD;\nDatabase = DBDorm; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasMaxLength(50)
                    .HasColumnName("price");
            });

            modelBuilder.Entity<Duty>(entity =>
            {
                entity.ToTable("Duty");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Coworker)
                    .HasMaxLength(50)
                    .HasColumnName("coworker");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .HasColumnName("name")
                    .IsFixedLength();

                entity.Property(e => e.Workday)
                    .HasMaxLength(50)
                    .HasColumnName("workday");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Duties)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DUTY_CATEGORY");
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.ToTable("Faculty");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Floor>(entity =>
            {
                entity.ToTable("Floor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChiefId).HasColumnName("chief_id");

                entity.Property(e => e.OpenKitchen)
                    .HasMaxLength(50)
                    .HasColumnName("open_kitchen");

                entity.HasOne(d => d.Chief)
                    .WithMany(p => p.Floors)
                    .HasForeignKey(d => d.ChiefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FLOOR_INHABITANT");
            });

            modelBuilder.Entity<Furniture>(entity =>
            {
                entity.ToTable("Furniture");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Furnitures)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FURNITURE_ROOM");
            });

            modelBuilder.Entity<Inhabitant>(entity =>
            {
                entity.ToTable("Inhabitant");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Act).HasColumnName("act");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.FacultyId).HasColumnName("faculty_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.YearStudy).HasColumnName("year_study");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Inhabitants)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_INHABITANT_CATEGORY");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Inhabitants)
                    .HasForeignKey(d => d.FacultyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_INHABITANT_FACULTY");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Inhabitants)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_INHABITANT_ROOM");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountPlace).HasColumnName("count_place");

                entity.Property(e => e.FloorId).HasColumnName("floor_id");

                entity.Property(e => e.Number)
                    .HasMaxLength(50)
                    .HasColumnName("number");

                entity.Property(e => e.Resident)
                    .HasColumnType("ntext")
                    .HasColumnName("resident");

                entity.HasOne(d => d.Floor)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.FloorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROOM_FLOOR");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
