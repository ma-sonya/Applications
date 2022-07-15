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
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DBDormContext(DbContextOptions<DBDormContext> options)
            : base(options)
        {
            Database.EnsureCreated();
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
                optionsBuilder.UseSqlServer("Server=LAPTOP-987VEQHD; Database=DBDorm; Trusted_Connection=True; MultipleActiveResultSets=true");
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

                entity.Property(e => e.FloorNumber).HasColumnName("floor_number");

                entity.Property(e => e.IsKitchenOpen)
                    .HasColumnType("bit")
                    .HasColumnName("open_kitchen");
            });

            modelBuilder.Entity<Furniture>(entity =>
            {
                entity.ToTable("Furniture");

                entity.Property(e => e.Number).HasColumnName("number");

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

                entity.Property(e => e.FloorNumber).HasColumnName("floor_id");

                entity.Property(e => e.RoomNumber)
                    .HasMaxLength(50)
                    .HasColumnName("number");

                entity.Property(e => e.Resident)
                    .HasColumnType("ntext")
                    .HasColumnName("resident");

                entity.HasOne(d => d.Floor)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.FloorNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROOM_FLOOR");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
