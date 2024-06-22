using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DO_AN.Models
{
    public partial class DOANContext : DbContext
    {
        public DOANContext()
        {
        }

        public DOANContext(DbContextOptions<DOANContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Coach> Coaches { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Discount> Discounts { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderTicket> OrderTickets { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Seat> Seats { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<Train> Trains { get; set; } = null!;
        public virtual DbSet<TrainRoute> TrainRoutes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-VHPDFFPP\\SQLEXPRESS;Initial Catalog=DOAN;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.IdAccount);

                entity.ToTable("Account");

                entity.Property(e => e.IdAccount)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Account");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(30);

                entity.Property(e => e.IdRole).HasColumnName("ID_Role");

                entity.Property(e => e.Password).HasMaxLength(30);

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

                entity.Property(e => e.IdCoach)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Coach");

                entity.Property(e => e.Category).HasMaxLength(20);

                entity.Property(e => e.NameCoach)
                    .HasMaxLength(30)
                    .HasColumnName("Name_Coach");

                entity.Property(e => e.SeatsQuantity).HasColumnName("Seats_Quantity");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.IdCus);

                entity.ToTable("Customer");

                entity.Property(e => e.IdCus)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Cus");

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .HasColumnName("Full_Name");

                entity.Property(e => e.IdAccount).HasColumnName("ID_Account");

                entity.Property(e => e.IdOrder).HasColumnName("ID_Order");

                entity.HasOne(d => d.IdAccountNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.IdAccount)
                    .HasConstraintName("FK_Customer_Account");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.IdOrder)
                    .HasConstraintName("FK_Customer_Order");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.IdDiscount);

                entity.ToTable("Discount");

                entity.Property(e => e.IdDiscount)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Discount");

                entity.Property(e => e.Information).HasMaxLength(200);

                entity.Property(e => e.NameDiscount)
                    .HasMaxLength(50)
                    .HasColumnName("Name_Discount");

                entity.Property(e => e.PercentDiscount).HasColumnName("Percent_Discount");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder);

                entity.ToTable("Order");

                entity.Property(e => e.IdOrder)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Order");

                entity.Property(e => e.DateOrder)
                    .HasColumnType("date")
                    .HasColumnName("Date_Order");

                entity.Property(e => e.IdDiscount).HasColumnName("ID_Discount");

                entity.Property(e => e.IdTicket).HasColumnName("ID_Ticket");

                entity.Property(e => e.UnitPrice).HasColumnName("Unit_Price");

                entity.HasOne(d => d.IdDiscountNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdDiscount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Discount");
            });

            modelBuilder.Entity<OrderTicket>(entity =>
            {
                entity.ToTable("Order_Ticket");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.IdOrder).HasColumnName("ID_Order");

                entity.Property(e => e.IdTicket).HasColumnName("ID_Ticket");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.OrderTickets)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Ticket_Order");

                entity.HasOne(d => d.IdTicketNavigation)
                    .WithMany(p => p.OrderTickets)
                    .HasForeignKey(d => d.IdTicket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Ticket_Ticket");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.ToTable("Role");

                entity.Property(e => e.IdRole)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Role");

                entity.Property(e => e.NameRole)
                    .HasMaxLength(20)
                    .HasColumnName("Name_Role");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.HasKey(e => e.IdSeat);

                entity.ToTable("Seat");

                entity.Property(e => e.IdSeat)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Seat");

                entity.Property(e => e.IdCoach).HasColumnName("ID_Coach");

                entity.Property(e => e.NameSeat)
                    .HasMaxLength(30)
                    .HasColumnName("Name_Seat");

                entity.HasOne(d => d.IdCoachNavigation)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.IdCoach)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Seat_Coach");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.IdTicket);

                entity.ToTable("Ticket");

                entity.Property(e => e.IdTicket)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Ticket");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.IdSeat).HasColumnName("ID_Seat");

                entity.Property(e => e.IdTrain).HasColumnName("ID_Train");

                entity.HasOne(d => d.IdTrainNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdTrain)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_Train");
            });

            modelBuilder.Entity<Train>(entity =>
            {
                entity.HasKey(e => e.IdTrain);

                entity.ToTable("Train");

                entity.Property(e => e.IdTrain)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Train");

                entity.Property(e => e.DateEnd)
                    .HasColumnType("date")
                    .HasColumnName("Date_End");

                entity.Property(e => e.DateStart)
                    .HasColumnType("date")
                    .HasColumnName("Date_Start");

                entity.Property(e => e.IdCoach).HasColumnName("ID_Coach");

                entity.Property(e => e.IdTrainRoute).HasColumnName("ID_TrainRoute");

                entity.Property(e => e.NameTrain)
                    .HasMaxLength(50)
                    .HasColumnName("Name_Train");

                entity.HasOne(d => d.IdCoachNavigation)
                    .WithMany(p => p.Trains)
                    .HasForeignKey(d => d.IdCoach)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Train_Coach");

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

                entity.Property(e => e.IdTrainRoute)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_TrainRoute");

                entity.Property(e => e.NameTrainRoute)
                    .HasMaxLength(50)
                    .HasColumnName("Name_TrainRoute");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
