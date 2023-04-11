﻿// <auto-generated />
using System;
using EF_Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EF_Core.Migrations
{
    [DbContext(typeof(CanteenContext))]
    partial class CanteenContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EF_Core.Models.Canteen", b =>
                {
                    b.Property<int>("CanteenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CanteenId"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CanteenId");

                    b.ToTable("Canteens");
                });

            modelBuilder.Entity("EF_Core.Models.Customer", b =>
                {
                    b.Property<string>("AUID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AUID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("EF_Core.Models.Meal", b =>
                {
                    b.Property<int>("MealId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MealId"));

                    b.Property<string>("MealType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("MealId");

                    b.HasIndex("MenuId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("EF_Core.Models.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MenuId"));

                    b.Property<int>("CanteenId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("MenuType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MenuId");

                    b.HasIndex("CanteenId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("EF_Core.Models.Rating", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RatingId"));

                    b.Property<int>("CanteenId")
                        .HasColumnType("int");

                    b.Property<string>("AUID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("RatingValue")
                        .HasColumnType("int");

                    b.HasKey("RatingId");

                    b.HasIndex("CanteenId");

                    b.HasIndex("AUID");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("EF_Core.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationId"));

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<int>("CanteenId")
                        .HasColumnType("int");

                    b.Property<string>("AUID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.HasKey("ReservationId");

                    b.HasIndex("CanteenId");

                    b.HasIndex("AUID");

                    b.HasIndex("MealId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("EF_Core.Models.Meal", b =>
                {
                    b.HasOne("EF_Core.Models.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("EF_Core.Models.Menu", b =>
                {
                    b.HasOne("EF_Core.Models.Canteen", "Canteen")
                        .WithMany()
                        .HasForeignKey("CanteenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Canteen");
                });

            modelBuilder.Entity("EF_Core.Models.Rating", b =>
                {
                    b.HasOne("EF_Core.Models.Canteen", "Canteen")
                        .WithMany()
                        .HasForeignKey("CanteenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EF_Core.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("AUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Canteen");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("EF_Core.Models.Reservation", b =>
                {
                    b.HasOne("EF_Core.Models.Canteen", "Canteen")
                        .WithMany()
                        .HasForeignKey("CanteenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EF_Core.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("AUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EF_Core.Models.Meal", "Meal")
                        .WithMany()
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Canteen");

                    b.Navigation("Customer");

                    b.Navigation("Meal");
                });
#pragma warning restore 612, 618
        }
    }
}
