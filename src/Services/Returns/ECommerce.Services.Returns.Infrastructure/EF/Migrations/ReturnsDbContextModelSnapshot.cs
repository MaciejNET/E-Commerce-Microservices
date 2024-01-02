﻿// <auto-generated />
using System;
using ECommerce.Services.Returns.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ECommerce.Services.Returns.Infrastructure.EF.Migrations
{
    [DbContext(typeof(ReturnsDbContext))]
    partial class ReturnsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ECommerce.Services.Returns.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CompletionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("OrderPlaced")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ECommerce.Services.Returns.Domain.Entities.OrderProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsReturn")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<string>("Sku")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("ECommerce.Services.Returns.Domain.Entities.Return", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("OrderProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("ReturnStatus")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("OrderProductId")
                        .IsUnique();

                    b.ToTable("Returns");
                });

            modelBuilder.Entity("ECommerce.Services.Returns.Domain.Entities.OrderProduct", b =>
                {
                    b.HasOne("ECommerce.Services.Returns.Domain.Entities.Order", null)
                        .WithMany("Products")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("ECommerce.Services.Returns.Domain.Entities.Return", b =>
                {
                    b.HasOne("ECommerce.Services.Returns.Domain.Entities.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.HasOne("ECommerce.Services.Returns.Domain.Entities.OrderProduct", "OrderProduct")
                        .WithOne()
                        .HasForeignKey("ECommerce.Services.Returns.Domain.Entities.Return", "OrderProductId");

                    b.Navigation("Order");

                    b.Navigation("OrderProduct");
                });

            modelBuilder.Entity("ECommerce.Services.Returns.Domain.Entities.Order", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}