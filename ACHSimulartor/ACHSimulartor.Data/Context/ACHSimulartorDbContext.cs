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

        #endregion

        #region Config

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            base.OnModelCreating(modelBuilder);
        }

        #endregion


    }
}
