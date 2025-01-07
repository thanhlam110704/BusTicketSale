﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DO_AN.Models
{
    public partial class DOAN_BoSungContext : DbContext
    {
        public DOAN_BoSungContext()
        {
        }

        public DOAN_BoSungContext(DbContextOptions<DOAN_BoSungContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Coach> Coaches { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Discount> Discounts { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Seat> Seats { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<Train> Trains { get; set; } = null!;
        public virtual DbSet<TrainRoute> TrainRoutes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-33S3RO1R;Initial Catalog=DOAN_BoSung;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.IdAccount);

                entity.ToTable("Account");

                entity.HasIndex(e => e.IdRole, "IX_Account_ID_Role");

                entity.Property(e => e.IdAccount).HasColumnName("ID_Account");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.IdRole).HasColumnName("ID_Role");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Role");
            });

            modelBuilder.Entity<Coach>(entity =>
            {
                entity.HasKey(e => e.IdCoach);

                entity.ToTable("Coach");

                entity.HasIndex(e => e.IdTrain, "IX_Coach_ID_Train");

                entity.Property(e => e.IdCoach).HasColumnName("ID_Coach");

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.IdTrain).HasColumnName("ID_Train");

                entity.Property(e => e.NameCoach)
                    .HasMaxLength(50)
                    .HasColumnName("Name_Coach");

                entity.Property(e => e.SeatsQuantity).HasColumnName("Seats_Quantity");

                entity.HasOne(d => d.IdTrainNavigation)
                    .WithMany(p => p.Coaches)
                    .HasForeignKey(d => d.IdTrain)
                    .HasConstraintName("FK_Coach_Train");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.IdCus);

                entity.ToTable("Customer");

                entity.HasIndex(e => e.IdAccount, "IX_Customer_ID_Account");

                entity.Property(e => e.IdCus)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Cus");

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .HasColumnName("Full_Name");

                entity.Property(e => e.IdAccount).HasColumnName("ID_Account");

                entity.HasOne(d => d.IdAccountNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.IdAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_Account");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.IdDiscount);

                entity.ToTable("Discount");

                entity.Property(e => e.IdDiscount).HasColumnName("ID_Discount");

                entity.Property(e => e.Information).HasMaxLength(150);

                entity.Property(e => e.NameDiscount)
                    .HasMaxLength(50)
                    .HasColumnName("Name_Discount");

                entity.Property(e => e.PercentDiscount).HasColumnName("Percent_Discount");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder);

                entity.ToTable("Order");

                entity.HasIndex(e => e.IdCus, "IX_Order_ID_Cus");

                entity.HasIndex(e => e.IdDiscount, "IX_Order_ID_Discount");

                entity.Property(e => e.IdOrder).HasColumnName("ID_Order");

                entity.Property(e => e.DateOrder)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Order");

                entity.Property(e => e.IdCus).HasColumnName("ID_Cus");

                entity.Property(e => e.IdDiscount).HasColumnName("ID_Discount");

                entity.Property(e => e.NameCus).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.UnitPrice).HasColumnName("Unit_Price");

                entity.HasOne(d => d.IdCusNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdCus)
                    .HasConstraintName("FK_Order_Customer");

                entity.HasOne(d => d.IdDiscountNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdDiscount)
                    .HasConstraintName("FK_Order_Discount");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.ToTable("Role");

                entity.Property(e => e.IdRole).HasColumnName("ID_Role");

                entity.Property(e => e.NameRole)
                    .HasMaxLength(50)
                    .HasColumnName("Name_Role");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.HasKey(e => e.IdSeat);

                entity.ToTable("Seat");

                entity.HasIndex(e => e.IdCoach, "IX_Seat_ID_Coach");

                entity.Property(e => e.IdSeat).HasColumnName("ID_Seat");

                entity.Property(e => e.IdCoach).HasColumnName("ID_Coach");

                entity.Property(e => e.NameSeat)
                    .HasMaxLength(50)
                    .HasColumnName("Name_Seat");

                entity.HasOne(d => d.IdCoachNavigation)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.IdCoach)
                    .HasConstraintName("FK_Seat_Coach");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.IdTicket);

                entity.ToTable("Ticket");

                entity.HasIndex(e => e.IdSeat, "IX_Ticket_ID_Seat");

                entity.HasIndex(e => e.IdTrain, "IX_Ticket_ID_Train");

                entity.Property(e => e.IdTicket).HasColumnName("ID_Ticket");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.IdOrder).HasColumnName("ID_Order");

                entity.Property(e => e.IdSeat).HasColumnName("ID_Seat");

                entity.Property(e => e.IdTrain).HasColumnName("ID_Train");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdOrder)
                    .HasConstraintName("FK_Ticket_Order");

                entity.HasOne(d => d.IdSeatNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdSeat)
                    .HasConstraintName("FK_Ticket_Seat");

                entity.HasOne(d => d.IdTrainNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdTrain)
                    .HasConstraintName("FK_Ticket_Train");
            });

            modelBuilder.Entity<Train>(entity =>
            {
                entity.HasKey(e => e.IdTrain);

                entity.ToTable("Train");

                entity.HasIndex(e => e.IdTrainRoute, "IX_Train_ID_TrainRoute");

                entity.Property(e => e.IdTrain).HasColumnName("ID_Train");

                entity.Property(e => e.CoefficientTrain).HasColumnType("decimal(14, 4)");

                entity.Property(e => e.DateStart)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Start");

                entity.Property(e => e.IdTrainRoute).HasColumnName("ID_TrainRoute");

                entity.Property(e => e.NameTrain)
                    .HasMaxLength(50)
                    .HasColumnName("Name_Train");

                entity.HasOne(d => d.IdTrainRouteNavigation)
                    .WithMany(p => p.Trains)
                    .HasForeignKey(d => d.IdTrainRoute)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Train_TrainRoute");
            });

            modelBuilder.Entity<TrainRoute>(entity =>
            {
                entity.HasKey(e => e.IdTrainRoute);

                entity.ToTable("TrainRoute");

                entity.Property(e => e.IdTrainRoute).HasColumnName("ID_TrainRoute");

                entity.Property(e => e.PointEnd)
                    .HasMaxLength(30)
                    .HasColumnName("Point_End");

                entity.Property(e => e.PointStart)
                    .HasMaxLength(30)
                    .HasColumnName("Point_Start");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
