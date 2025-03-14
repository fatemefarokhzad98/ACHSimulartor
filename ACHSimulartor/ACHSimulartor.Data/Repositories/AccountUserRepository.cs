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
    public class AccountUserRepository(ACHSimulartorDbContext _context) : IAccountUserRepository
    {
        public async Task<AccountUser?> GetAccountUserByShebaNumber(string shebaNumber)
        {
            return await _context.AccountUsers.Where(x => x.ShebaNumber == shebaNumber).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> HasSufficientBalanceAsync(string shebaNo, decimal price)
        {
            return await _context.AccountUsers.Where(x=>x.ShebaNumber==shebaNo).AnyAsync(money=>money.AccountBalanc==price ||money.AccountBalanc>price);
        }

        public async Task<bool> UpdateAccountUserAsync(AccountUser model)
        {
            AccountUser? entity = await _context.AccountUsers.Where(x => x.ShebaNumber == model.ShebaNumber).FirstOrDefaultAsync();
            if (entity is null)
                return false;
            entity.Transaction = model.Transaction;
            entity.AccountBalanc = model.AccountBalanc;
            _context.AccountUsers.Update(entity);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
