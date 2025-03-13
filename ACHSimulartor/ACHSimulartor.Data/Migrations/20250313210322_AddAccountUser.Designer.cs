﻿// <auto-generated />
using System;
using ACHSimulartor.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ACHSimulartor.Data.Migrations
{
    [DbContext(typeof(ACHSimulartorDbContext))]
    [Migration("20250313210322_AddAccountUser")]
    partial class AddAccountUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ACHSimulartor.Domain.Entites.AccountUser", b =>
                {
                    b.Property<string>("ShebaNumber")
                        .HasMaxLength(26)
                        .HasColumnType("nvarchar(26)");

                    b.Property<decimal>("AccountBalanc")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Transaction")
                        .HasColumnType("int");

                    b.HasKey("ShebaNumber");

                    b.ToTable("AccountUsers");
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Staus")
                        .HasColumnType("int");

                    b.Property<string>("ToShebaNumber")
                        .IsRequired()
                        .HasMaxLength(26)
                        .HasColumnType("nvarchar(26)");

                    b.HasKey("Id");

                    b.HasIndex("FromShebaNumber");

                    b.ToTable("TransferRequests");
                });

            modelBuilder.Entity("ACHSimulartor.Domain.Entites.TransferRequest", b =>
                {
                    b.HasOne("ACHSimulartor.Domain.Entites.AccountUser", "AccountUser")
                        .WithMany("TransferRequests")
                        .HasForeignKey("FromShebaNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountUser");
                });

            modelBuilder.Entity("ACHSimulartor.Domain.Entites.AccountUser", b =>
                {
                    b.Navigation("TransferRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
