﻿// <auto-generated />
using System;
using ComplexModel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ComplexModel.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ComplexModel.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,0)");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("ComplexModel.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ComplexModel.Models.OrderedItem", b =>
                {
                    b.Property<int?>("OrderId_FK")
                        .HasColumnType("int");

                    b.Property<int?>("ItemId_Fk")
                        .HasColumnType("int");

                    b.Property<int?>("UnitId_Fk")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("Sub_Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("customerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderId_FK", "ItemId_Fk", "UnitId_Fk");

                    b.HasIndex("ItemId_Fk");

                    b.ToTable("OrderedItems");
                });

            modelBuilder.Entity("ComplexModel.Models.Unit", b =>
                {
                    b.Property<int>("UnitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UnitId"));

                    b.Property<string>("UnitType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UnitId");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("ComplexModel.Models.UnitItem", b =>
                {
                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("Quatity")
                        .HasColumnType("int");

                    b.HasKey("UnitId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("UnitItems");
                });

            modelBuilder.Entity("ComplexModel.Models.OrderedItem", b =>
                {
                    b.HasOne("ComplexModel.Models.Item", "Item")
                        .WithMany("OrderedItems")
                        .HasForeignKey("ItemId_Fk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComplexModel.Models.Unit", "Unit")
                        .WithMany("OrderedItems")
                        .HasForeignKey("ItemId_Fk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComplexModel.Models.Order", "Order")
                        .WithMany("OrderItem")
                        .HasForeignKey("OrderId_FK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Order");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("ComplexModel.Models.UnitItem", b =>
                {
                    b.HasOne("ComplexModel.Models.Item", "Item")
                        .WithMany("UnitItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComplexModel.Models.Unit", "Unit")
                        .WithMany("UnitItems")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("ComplexModel.Models.Item", b =>
                {
                    b.Navigation("OrderedItems");

                    b.Navigation("UnitItems");
                });

            modelBuilder.Entity("ComplexModel.Models.Order", b =>
                {
                    b.Navigation("OrderItem");
                });

            modelBuilder.Entity("ComplexModel.Models.Unit", b =>
                {
                    b.Navigation("OrderedItems");

                    b.Navigation("UnitItems");
                });
#pragma warning restore 612, 618
        }
    }
}
