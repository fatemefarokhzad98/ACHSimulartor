using ACHSimulartor.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHSimulartor.Data.Seeds
{
    public static class DataSeeding
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            #region User
            User user1 = new()
            {
                AccountBalance = 5000,
                BankAccountBalance = 50000,
                UserShebaNumber = "IR510120010000002333219753"
            };
            modelBuilder.Entity<User>().HasData(user1,
                  new User
                  {
                     AccountBalance = 5000,
                     BankAccountBalance = 50000,
                     UserShebaNumber = "IR650540126030102398779600"



                 });
            #endregion
        }
    }
}