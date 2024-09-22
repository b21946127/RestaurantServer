﻿// <auto-generated />
using System;
using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240921101346_mig1")]
    partial class mig1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EntityLayer.Concrete.Integration", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int?>("MenuItemId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MenuItemId");

                    b.ToTable("Integrations");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Menu", b =>
                {
                    b.Property<int>("DayOfWeek")
                        .HasColumnType("integer");

                    b.HasKey("DayOfWeek");

                    b.ToTable("Menus");

                    b.HasData(
                        new
                        {
                            DayOfWeek = 1
                        },
                        new
                        {
                            DayOfWeek = 2
                        },
                        new
                        {
                            DayOfWeek = 3
                        },
                        new
                        {
                            DayOfWeek = 4
                        },
                        new
                        {
                            DayOfWeek = 5
                        },
                        new
                        {
                            DayOfWeek = 6
                        },
                        new
                        {
                            DayOfWeek = 7
                        });
                });

            modelBuilder.Entity("EntityLayer.Concrete.MenuCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("MenuDayOfWeek")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MenuDayOfWeek");

                    b.ToTable("MenuCategories");
                });

            modelBuilder.Entity("EntityLayer.Concrete.MenuCategoryMenuItem", b =>
                {
                    b.Property<int>("MenuCategoryId")
                        .HasColumnType("integer");

                    b.Property<int>("MenuItemId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.HasKey("MenuCategoryId", "MenuItemId");

                    b.HasIndex("MenuItemId");

                    b.ToTable("MenuCategoryItems");
                });

            modelBuilder.Entity("EntityLayer.Concrete.MenuItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("MenuItemId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Portion")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("MenuItemId");

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("EntityLayer.Concrete.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("EntityLayer.Concrete.OrderItemMenuItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .HasColumnType("integer");

                    b.Property<int>("MenuItemId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.HasKey("OrderItemId", "MenuItemId");

                    b.HasIndex("MenuItemId");

                    b.ToTable("OrderItemMenuItems");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Integration", b =>
                {
                    b.HasOne("EntityLayer.Concrete.MenuItem", null)
                        .WithMany("Integrations")
                        .HasForeignKey("MenuItemId");
                });

            modelBuilder.Entity("EntityLayer.Concrete.MenuCategory", b =>
                {
                    b.HasOne("EntityLayer.Concrete.Menu", "Menu")
                        .WithMany("MenuCategories")
                        .HasForeignKey("MenuDayOfWeek")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("EntityLayer.Concrete.MenuCategoryMenuItem", b =>
                {
                    b.HasOne("EntityLayer.Concrete.MenuCategory", "MenuCategory")
                        .WithMany("MenuCategoryItems")
                        .HasForeignKey("MenuCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityLayer.Concrete.MenuItem", "MenuItem")
                        .WithMany("MenuCategoryItems")
                        .HasForeignKey("MenuItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuCategory");

                    b.Navigation("MenuItem");
                });

            modelBuilder.Entity("EntityLayer.Concrete.MenuItem", b =>
                {
                    b.HasOne("EntityLayer.Concrete.MenuItem", null)
                        .WithMany("SubItems")
                        .HasForeignKey("MenuItemId");
                });

            modelBuilder.Entity("EntityLayer.Concrete.OrderItem", b =>
                {
                    b.HasOne("EntityLayer.Concrete.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("EntityLayer.Concrete.OrderItemMenuItem", b =>
                {
                    b.HasOne("EntityLayer.Concrete.MenuItem", "MenuItem")
                        .WithMany("OrderItemMenuItems")
                        .HasForeignKey("MenuItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityLayer.Concrete.OrderItem", "OrderItem")
                        .WithMany("OrderItemMenuItems")
                        .HasForeignKey("OrderItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuItem");

                    b.Navigation("OrderItem");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Menu", b =>
                {
                    b.Navigation("MenuCategories");
                });

            modelBuilder.Entity("EntityLayer.Concrete.MenuCategory", b =>
                {
                    b.Navigation("MenuCategoryItems");
                });

            modelBuilder.Entity("EntityLayer.Concrete.MenuItem", b =>
                {
                    b.Navigation("Integrations");

                    b.Navigation("MenuCategoryItems");

                    b.Navigation("OrderItemMenuItems");

                    b.Navigation("SubItems");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("EntityLayer.Concrete.OrderItem", b =>
                {
                    b.Navigation("OrderItemMenuItems");
                });
#pragma warning restore 612, 618
        }
    }
}
