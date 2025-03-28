﻿// <auto-generated />
using System;
using ACHSimulartor.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ACHSimulartor.Data.Migrations
{
    [DbContext(typeof(ACHSimulartorDbContext))]
    partial class ACHSimulartorDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ACHSimulartor.Domain.Entites.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Price")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18, 2)")
                        .HasDefaultValue(0m);

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.Property<int>("TransferRequestId")
                        .HasColumnType("int");

                    b.Property<string>("UserShebaNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(26)");

                    b.HasKey("Id");

                    b.HasIndex("TransferRequestId");

                    b.HasIndex("UserShebaNumber");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("ACHSimulartor.Domain.Entites.TransferRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FromShebaNumber")
                        .IsRequired()
                        .HasMaxLength(26)
                        .HasColumnType("nvarchar(26)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("ToShebaNumber")
                        .IsRequired()
                        .HasMaxLength(26)
                        .HasColumnType("nvarchar(26)");

                    b.Property<string>("UserShebaNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(26)");

                    b.HasKey("Id");

                    b.HasIndex("UserShebaNumber");

                    b.ToTable("TransferRequests");
                });

            modelBuilder.Entity("ACHSimulartor.Domain.Entites.User", b =>
                {
                    b.Property<string>("UserShebaNumber")
                        .HasMaxLength(26)
                        .HasColumnType("nvarchar(26)");

                    b.Property<decimal>("AccountBalance")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.Property<decimal>("BankAccountBalance")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.HasKey("UserShebaNumber");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserShebaNumber = "IR510120010000002333219753",
                            AccountBalance = 5000m,
                            BankAccountBalance = 50000m
                        },
                        new
                        {
                            UserShebaNumber = "IR650540126030102398779600",
                            AccountBalance = 5000m,
                            BankAccountBalance = 50000m
                        });
                });

            modelBuilder.Entity("ACHSimulartor.Domain.Entites.Transaction", b =>
                {
                    b.HasOne("ACHSimulartor.Domain.Entites.TransferRequest", "TransferRequest")
                        .WithMany("Transactions")
                        .HasForeignKey("TransferRequestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ACHSimulartor.Domain.Entites.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserShebaNumber")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TransferRequest");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ACHSimulartor.Domain.Entites.TransferRequest", b =>
                {
                    b.HasOne("ACHSimulartor.Domain.Entites.User", "User")
                        .WithMany("TransferRequests")
                        .HasForeignKey("UserShebaNumber")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ACHSimulartor.Domain.Entites.TransferRequest", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("ACHSimulartor.Domain.Entites.User", b =>
                {
                    b.Navigation("Transactions");

                    b.Navigation("TransferRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
