using ACHSimulartor.Data.Context;
using ACHSimulartor.Domain.Entites;
using ACHSimulartor.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHSimulartor.Data.Repositories
{
    public class UserRepository(ACHSimulartorDbContext _context) : IUserRepository
    {
        public async Task<User?> GetAccountUserAsync(string shebaNumber)
        {
            return await _context.Users.Where(x => x.UserShebaNumber == shebaNumber).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> HasSufficientBalanceAsync(string shebaNo, decimal price)
        {
            return await _context.Users.Where(x => x.UserShebaNumber == shebaNo).AnyAsync(money => money.AccountBalance == price || money.AccountBalance > price);
        }

        public async Task<bool> UpdateAccountUserAsync(User model)
        {
            var entity = await _context.Users.Where(x => x.UserShebaNumber == model.UserShebaNumber).FirstOrDefaultAsync();
            if (entity is null)
                return false;
            entity.AccountBalance = model.AccountBalance;
            entity.BankAccountBalance = model.BankAccountBalance;
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        
    }
}
