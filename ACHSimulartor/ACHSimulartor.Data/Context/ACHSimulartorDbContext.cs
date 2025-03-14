using ACHSimulartor.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ACHSimulartor.Data.Context
{
    public class ACHSimulartorDbContext(DbContextOptions<ACHSimulartorDbContext> options) : DbContext(options)
    {
        #region DbSet
        public DbSet<TransferRequest> TransferRequests { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        #endregion

        #region Config

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region TransferRequestConfig
            modelBuilder.Entity<TransferRequest>()
             .HasKey(x => x.Id);
            modelBuilder.Entity<TransferRequest>()
           .Property(b => b.FromShebaNumber)
           .IsRequired()
           .HasMaxLength(26);
            modelBuilder.Entity<TransferRequest>()
           .Property(b => b.ToShebaNumber)
           .IsRequired()
           .HasMaxLength(26);

            modelBuilder.Entity<TransferRequest>()
            .Property(b => b.Price)
             .HasColumnType("decimal(18, 2)")
             .IsRequired();
            modelBuilder.Entity<TransferRequest>()
            .Property(b => b.Note)
             .IsRequired();
            #endregion

            #region UserConfig
            modelBuilder.Entity<User>()
            .HasKey(x => x.UserShebaNumber);
            modelBuilder.Entity<User>()
           .Property(b => b.UserShebaNumber)
           .IsRequired()
           .HasMaxLength(26);
            modelBuilder.Entity<User>()
            .Property(b => b.AccountBalance)
            .HasColumnType("decimal(18, 2)")
            .HasDefaultValue(0);
            
            #endregion

            #region Transaction
            modelBuilder.Entity<Transaction>()
            .HasKey(x => x.Id);
            modelBuilder.Entity<Transaction>()
            .Property(x => x.Price)
            .HasColumnType("decimal(18, 2)")
            .HasDefaultValue(0);

            #endregion

            #region Relation
            modelBuilder.Entity<TransferRequest>()
           .HasOne(x => x.User)
           .WithMany(x => x.TransferRequests)
            .HasForeignKey(x => x.UserShebaNumber);

            modelBuilder.Entity<Transaction>()
             .HasOne(x => x.TransferRequest)
            .WithMany(x => x.Transactions)
            .HasForeignKey(x => x.TransferRequestId);

            modelBuilder.Entity<Transaction>()
           .HasOne(x => x.User)
           .WithMany(x => x.Transactions)
           .HasForeignKey(x => x.UserShebaNumber);
            #endregion

            base.OnModelCreating(modelBuilder);
        }

        #endregion


    }
}
