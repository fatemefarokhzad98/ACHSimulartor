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
        public DbSet<AccountUser> AccountUsers { get; set; }
        public DbSet<Bank> Banks { get; set; }

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
             .IsRequired();
            modelBuilder.Entity<TransferRequest>()
            .Property(b => b.Note)
             .IsRequired();
            #endregion

            #region AccountUserConfig
            modelBuilder.Entity<AccountUser>()
            .HasKey(x => x.ShebaNumber);
            modelBuilder.Entity<AccountUser>()
           .Property(b => b.ShebaNumber)
           .IsRequired()
           .HasMaxLength(26);
            modelBuilder.Entity<AccountUser>()
            .Property(b => b.AccountBalanc)
           .HasDefaultValue(0);

            #endregion
            #region Bank
            modelBuilder.Entity<Bank>()
            .HasKey(x => x.BankCode);
            modelBuilder.Entity<Bank>()
           .Property(x => x.BankCode)
           .HasMaxLength(3)
           .IsRequired();
            modelBuilder.Entity<Bank>()
            .Property(x => x.BankAccountBalanc)
            .HasDefaultValue(0);
           
            #endregion

            #region Relation
            modelBuilder.Entity<TransferRequest>()
           .HasOne(x => x.AccountUser)
           .WithMany(x => x.TransferRequests)
           .HasForeignKey(x => x.FromShebaNumber);
            modelBuilder.Entity<TransferRequest>()
           .HasOne(x => x.Bank)
           .WithMany(x => x.TransferRequests)
           .HasForeignKey(x => x.BankCode);
            #endregion

            base.OnModelCreating(modelBuilder);
        }

        #endregion


    }
}
