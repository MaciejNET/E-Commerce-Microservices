﻿// <auto-generated />
using System;
using ECommerce.Modules.Orders.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ECommerce.Modules.Orders.Infrastructure.EF.Migrations
{
    [DbContext(typeof(OrdersDbContext))]
    partial class OrdersDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DiscountsProducts", b =>
                {
                    b.Property<Guid>("DiscountId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductsId")
                        .HasColumnType("uuid");

                    b.HasKey("DiscountId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("DiscountsProducts");
                });

            modelBuilder.Entity("ECommerce.Services.Orders.Domain.Carts.Entities.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("PreferredCurrency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("ECommerce.Services.Orders.Domain.Carts.Entities.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CartId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("ECommerce.Services.Orders.Domain.Carts.Entities.CheckoutCart", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("Currency")
                        .HasColumnType("integer");

                    b.Property<Guid?>("DiscountId")
                        .HasColumnType("uuid");

                    b.Property<int>("Payment")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DiscountId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("CheckoutCarts");
                });

            modelBuilder.Entity("ECommerce.Services.Orders.Domain.Carts.Entities.CheckoutCartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CheckoutCartId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CheckoutCartId");

                    b.ToTable("CheckoutCartItems");
                });

            modelBuilder.Entity("ECommerce.Services.Orders.Domain.Carts.Entities.Discount", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<int>("Percentage")
                        .HasColumnType("integer");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("ECommerce.Services.Orders.Domain.Carts.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Sku")
                        .HasColumnType("text");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("integer");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Sku")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ECommerce.Services.Orders.Domain.Orders.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CompletionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("integer");

                    b.Property<DateTime>("PlaceDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DiscountsProducts", b =>
                {
                    b.HasOne("ECommerce.Services.Orders.Domain.Carts.Entities.Discount", null)
                        .WithMany()
                        .HasForeignKey("DiscountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ECommerce.Services.Orders.Domain.Carts.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ECommerce.Services.Orders.Domain.Carts.Entities.CartItem", b =>
                {
                    b.HasOne("ECommerce.Services.Orders.Domain.Carts.Entities.Cart", null)
                        .WithMany("Items")
                        .HasForeignKey("CartId");

                    b.HasOne("ECommerce.Services.Orders.Domain.Carts.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ECommerce.Services.Orders.Domain.Carts.Entities.CheckoutCart", b =>
                {
                    b.HasOne("ECommerce.Services.Orders.Domain.Carts.Entities.Discount", "Discount")
                        .WithMany()
                        .HasForeignKey("DiscountId");

                    b.OwnsOne("ECommerce.Services.Orders.Domain.Shared.ValueObjects.Shipment", "Shipment", b1 =>
                        {
                            b1.Property<Guid>("CheckoutCartId")
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .HasColumnType("text");

                            b1.Property<string>("ReceiverFullName")
                                .HasColumnType("text");

                            b1.Property<string>("StreetName")
                                .HasColumnType("text");

                            b1.Property<int>("StreetNumber")
                                .HasColumnType("integer");

                            b1.HasKey("CheckoutCartId");

                            b1.ToTable("CheckoutCarts");

                            b1.WithOwner()
                                .HasForeignKey("CheckoutCartId");
                        });

                    b.Navigation("Discount");

                    b.Navigation("Shipment");
                });

            modelBuilder.Entity("ECommerce.Services.Orders.Domain.Carts.Entities.CheckoutCartItem", b =>
                {
                    b.HasOne("ECommerce.Services.Orders.Domain.Carts.Entities.CheckoutCart", null)
                        .WithMany("Items")
                        .HasForeignKey("CheckoutCartId");

                    b.OwnsOne("ECommerce.Shared.Abstractions.Kernel.Types.Price", "DiscountedPrice", b1 =>
                        {
                            b1.Property<Guid>("CheckoutCartItemId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 2)
                                .HasColumnType("numeric(18,2)");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("CheckoutCartItemId");

                            b1.ToTable("CheckoutCartItems");

                            b1.WithOwner()
                                .HasForeignKey("CheckoutCartItemId");
                        });

                    b.OwnsOne("ECommerce.Shared.Abstractions.Kernel.Types.Price", "Price", b1 =>
                        {
                            b1.Property<Guid>("CheckoutCartItemId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 2)
                                .HasColumnType("numeric(18,2)");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("CheckoutCartItemId");

                            b1.ToTable("CheckoutCartItems");

                            b1.WithOwner()
                                .HasForeignKey("CheckoutCartItemId");
                        });

                    b.Navigation("DiscountedPrice");

                    b.Navigation("Price");
                });

            modelBuilder.Entity("ECommerce.Services.Orders.Domain.Carts.Entities.Product", b =>
                {
                    b.OwnsOne("ECommerce.Shared.Abstractions.Kernel.Types.Price", "DiscountedPrice", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 2)
                                .HasColumnType("numeric(18,2)");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("ECommerce.Shared.Abstractions.Kernel.Types.Price", "StandardPrice", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 2)
                                .HasColumnType("numeric(18,2)");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("DiscountedPrice");

                    b.Navigation("StandardPrice");
                });

            modelBuilder.Entity("ECommerce.Services.Orders.Domain.Orders.Entities.Order", b =>
                {
                    b.OwnsOne("ECommerce.Services.Orders.Domain.Shared.ValueObjects.Shipment", "Shipment", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .HasColumnType("text");

                            b1.Property<string>("ReceiverFullName")
                                .HasColumnType("text");

                            b1.Property<string>("StreetName")
                                .HasColumnType("text");

                            b1.Property<int>("StreetNumber")
                                .HasColumnType("integer");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.OwnsMany("ECommerce.Services.Orders.Domain.Orders.ValueObjects.OrderLine", "Lines", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Currency")
                                .HasColumnType("text");

                            b1.Property<string>("Name")
                                .HasColumnType("text");

                            b1.Property<int>("OrderLineNumber")
                                .HasColumnType("integer");

                            b1.Property<int>("Quantity")
                                .HasColumnType("integer");

                            b1.Property<string>("Sku")
                                .HasColumnType("text");

                            b1.Property<decimal>("UnitPrice")
                                .HasPrecision(18, 2)
                                .HasColumnType("numeric(18,2)");

                            b1.HasKey("OrderId", "Id");

                            b1.ToTable("OrderLine");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("Lines");

                    b.Navigation("Shipment");
                });

            modelBuilder.Entity("ECommerce.Services.Orders.Domain.Carts.Entities.Cart", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("ECommerce.Services.Orders.Domain.Carts.Entities.CheckoutCart", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
