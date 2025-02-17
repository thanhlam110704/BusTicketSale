﻿// <auto-generated />
using System;
using DO_AN.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DO_AN.Migrations
{
    [DbContext(typeof(DOAN_BoSungContext))]
    partial class DOANContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DO_AN.Models.Account", b =>
                {
                    b.Property<int>("IdAccount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Account");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAccount"), 1L, 1);

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("IdRole")
                        .HasColumnType("int")
                        .HasColumnName("ID_Role");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool?>("Sex")
                        .HasColumnType("bit");

                    b.Property<bool?>("EmailConformed")
                        .HasColumnType("bit");

                    b.HasKey("IdAccount");

                    b.HasIndex(new[] { "IdRole" }, "IX_Account_ID_Role");

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("DO_AN.Models.Coach", b =>
                {
                    b.Property<int>("IdCoach")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Coach");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCoach"), 1L, 1);

                    b.Property<double?>("BasicPrice")
                        .HasColumnType("float");

                    b.Property<string>("Category")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("IdTrain")
                        .HasColumnType("int")
                        .HasColumnName("ID_Train");

                    b.Property<string>("NameCoach")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Name_Coach");

                    b.Property<int?>("SeatsQuantity")
                        .HasColumnType("int")
                        .HasColumnName("Seats_Quantity");

                    b.HasKey("IdCoach");

                    b.HasIndex("IdTrain");

                    b.ToTable("Coach", (string)null);
                });

            modelBuilder.Entity("DO_AN.Models.Customer", b =>
                {
                    b.Property<int>("IdCus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Cus");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCus"), 1L, 1);

                    b.Property<string>("FullName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Full_Name");

                    b.Property<int>("IdAccount")
                        .HasColumnType("int")
                        .HasColumnName("ID_Account");

                    b.HasKey("IdCus");

                    b.HasIndex(new[] { "IdAccount" }, "IX_Customer_ID_Account");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("DO_AN.Models.Discount", b =>
                {
                    b.Property<int>("IdDiscount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Discount");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDiscount"), 1L, 1);

                    b.Property<string>("Information")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("NameDiscount")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Name_Discount");

                    b.Property<int?>("PercentDiscount")
                        .HasColumnType("int")
                        .HasColumnName("Percent_Discount");

                    b.HasKey("IdDiscount");

                    b.ToTable("Discount", (string)null);
                });

            modelBuilder.Entity("DO_AN.Models.Order", b =>
                {
                    b.Property<int>("IdOrder")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Order");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdOrder"), 1L, 1);

                    b.Property<DateTime?>("DateOrder")
                        .HasColumnType("date")
                        .HasColumnName("Date_Order");

                    b.Property<int?>("IdCus")
                        .HasColumnType("int")
                        .HasColumnName("ID_Cus");

                    b.Property<int?>("IdDiscount")
                        .HasColumnType("int")
                        .HasColumnName("ID_Discount");

                    b.Property<int>("IdTicket")
                        .HasColumnType("int")
                        .HasColumnName("ID_Ticket");

                    b.Property<string>("NameCus")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<double?>("UnitPrice")
                        .HasColumnType("float")
                        .HasColumnName("Unit_Price");

                    b.HasKey("IdOrder");

                    b.HasIndex("IdCus");

                    b.HasIndex("IdDiscount");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("DO_AN.Models.OrderTicket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdOrder")
                        .HasColumnType("int")
                        .HasColumnName("ID_Order");

                    b.Property<int>("IdTicket")
                        .HasColumnType("int")
                        .HasColumnName("ID_Ticket");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "IdOrder" }, "IX_Order_Ticket_ID_Order");

                    b.HasIndex(new[] { "IdTicket" }, "IX_Order_Ticket_ID_Ticket");

                    b.ToTable("Order_Ticket", (string)null);
                });

            modelBuilder.Entity("DO_AN.Models.Role", b =>
                {
                    b.Property<int>("IdRole")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Role");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRole"), 1L, 1);

                    b.Property<string>("NameRole")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Name_Role");

                    b.HasKey("IdRole");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("DO_AN.Models.Seat", b =>
                {
                    b.Property<int>("IdSeat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Seat");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSeat"), 1L, 1);

                    b.Property<int?>("IdCoach")
                        .HasColumnType("int")
                        .HasColumnName("ID_Coach");

                    b.Property<string>("NameSeat")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Name_Seat");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.HasKey("IdSeat");

                    b.HasIndex("IdCoach");

                    b.ToTable("Seat", (string)null);
                });

            modelBuilder.Entity("DO_AN.Models.Ticket", b =>
                {
                    b.Property<int>("IdTicket")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Ticket");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTicket"), 1L, 1);

                    b.Property<DateTime?>("Date")
                        .HasColumnType("date");

                    b.Property<int>("IdSeat")
                        .HasColumnType("int")
                        .HasColumnName("ID_Seat");

                    b.Property<int>("IdTrain")
                        .HasColumnType("int")
                        .HasColumnName("ID_Train");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.HasKey("IdTicket");

                    b.HasIndex(new[] { "IdSeat" }, "IX_Ticket_ID_Seat");

                    b.HasIndex(new[] { "IdTrain" }, "IX_Ticket_ID_Train");

                    b.ToTable("Ticket", (string)null);
                });

            modelBuilder.Entity("DO_AN.Models.Train", b =>
                {
                    b.Property<int>("IdTrain")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Train");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTrain"), 1L, 1);

                    b.Property<decimal?>("CoefficientTrain")
                        .HasColumnType("decimal(14,4)");

                    b.Property<DateTime?>("DateStart")
                        .HasColumnType("date")
                        .HasColumnName("Date_Start");

                    b.Property<int>("IdTrainRoute")
                        .HasColumnType("int")
                        .HasColumnName("ID_TrainRoute");

                    b.Property<string>("NameTrain")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Name_Train");

                    b.HasKey("IdTrain");

                    b.HasIndex(new[] { "IdTrainRoute" }, "IX_Train_ID_TrainRoute");

                    b.ToTable("Train", (string)null);
                });

            modelBuilder.Entity("DO_AN.Models.TrainRoute", b =>
                {
                    b.Property<int>("IdTrainRoute")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_TrainRoute");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTrainRoute"), 1L, 1);

                    b.Property<string>("PointEnd")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("Point_End");

                    b.Property<string>("PointStart")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("Point_Start");

                    b.HasKey("IdTrainRoute");

                    b.ToTable("TrainRoute", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DO_AN.Models.Account", b =>
                {
                    b.HasOne("DO_AN.Models.Role", "IdRoleNavigation")
                        .WithMany("Accounts")
                        .HasForeignKey("IdRole")
                        .IsRequired()
                        .HasConstraintName("FK_Account_Role");

                    b.Navigation("IdRoleNavigation");
                });

            modelBuilder.Entity("DO_AN.Models.Coach", b =>
                {
                    b.HasOne("DO_AN.Models.Train", "IdTrainNavigation")
                        .WithMany("Coaches")
                        .HasForeignKey("IdTrain")
                        .HasConstraintName("FK_Coach_Train");

                    b.Navigation("IdTrainNavigation");
                });

            modelBuilder.Entity("DO_AN.Models.Customer", b =>
                {
                    b.HasOne("DO_AN.Models.Account", "IdAccountNavigation")
                        .WithMany("Customers")
                        .HasForeignKey("IdAccount")
                        .IsRequired()
                        .HasConstraintName("FK_Customer_Account");

                    b.Navigation("IdAccountNavigation");
                });

            modelBuilder.Entity("DO_AN.Models.Order", b =>
                {
                    b.HasOne("DO_AN.Models.Customer", "IdCusNavigation")
                        .WithMany("Orders")
                        .HasForeignKey("IdCus")
                        .HasConstraintName("FK_Order_Customer");

                    b.HasOne("DO_AN.Models.Discount", "IdDiscountNavigation")
                        .WithMany("Orders")
                        .HasForeignKey("IdDiscount")
                        .HasConstraintName("FK_Order_Discount");

                    b.Navigation("IdCusNavigation");

                    b.Navigation("IdDiscountNavigation");
                });

            modelBuilder.Entity("DO_AN.Models.OrderTicket", b =>
                {
                    b.HasOne("DO_AN.Models.Order", "IdOrderNavigation")
                        .WithMany("OrderTickets")
                        .HasForeignKey("IdOrder")
                        .IsRequired()
                        .HasConstraintName("FK_Order_Ticket_Order");

                    b.HasOne("DO_AN.Models.Ticket", "IdTicketNavigation")
                        .WithMany("OrderTickets")
                        .HasForeignKey("IdTicket")
                        .IsRequired()
                        .HasConstraintName("FK_Order_Ticket_Ticket");

                    b.Navigation("IdOrderNavigation");

                    b.Navigation("IdTicketNavigation");
                });

            modelBuilder.Entity("DO_AN.Models.Seat", b =>
                {
                    b.HasOne("DO_AN.Models.Coach", "IdCoachNavigation")
                        .WithMany("Seats")
                        .HasForeignKey("IdCoach")
                        .HasConstraintName("FK_Seat_Coach");

                    b.Navigation("IdCoachNavigation");
                });

            modelBuilder.Entity("DO_AN.Models.Ticket", b =>
                {
                    b.HasOne("DO_AN.Models.Seat", "IdSeatNavigation")
                        .WithMany("Tickets")
                        .HasForeignKey("IdSeat")
                        .IsRequired()
                        .HasConstraintName("FK_Ticket_Seat");

                    b.HasOne("DO_AN.Models.Train", "IdTrainNavigation")
                        .WithMany("Tickets")
                        .HasForeignKey("IdTrain")
                        .IsRequired()
                        .HasConstraintName("FK_Ticket_Train");

                    b.Navigation("IdSeatNavigation");

                    b.Navigation("IdTrainNavigation");
                });

            modelBuilder.Entity("DO_AN.Models.Train", b =>
                {
                    b.HasOne("DO_AN.Models.TrainRoute", "IdTrainRouteNavigation")
                        .WithMany("Trains")
                        .HasForeignKey("IdTrainRoute")
                        .IsRequired()
                        .HasConstraintName("FK_Train_TrainRoute");

                    b.Navigation("IdTrainRouteNavigation");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DO_AN.Models.Account", b =>
                {
                    b.Navigation("Customers");
                });

            modelBuilder.Entity("DO_AN.Models.Coach", b =>
                {
                    b.Navigation("Seats");
                });

            modelBuilder.Entity("DO_AN.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("DO_AN.Models.Discount", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("DO_AN.Models.Order", b =>
                {
                    b.Navigation("OrderTickets");
                });

            modelBuilder.Entity("DO_AN.Models.Role", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("DO_AN.Models.Seat", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("DO_AN.Models.Ticket", b =>
                {
                    b.Navigation("OrderTickets");
                });

            modelBuilder.Entity("DO_AN.Models.Train", b =>
                {
                    b.Navigation("Coaches");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("DO_AN.Models.TrainRoute", b =>
                {
                    b.Navigation("Trains");
                });
#pragma warning restore 612, 618
        }
    }
}
