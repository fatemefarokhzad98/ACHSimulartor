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
    public class BankRepository(ACHSimulartorDbContext _context) : IBankRepository
    {
        public async Task<Bank?> GetBankByBankCodeAsync(int bankCode)
        {
            return await _context.Banks.Where(x => x.BankCode == bankCode).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateBankAccountBalanceAsync(Bank model)
        {
            var entity = await _context.Banks.Where(x => x.BankCode == model.BankCode).FirstOrDefaultAsync();
            if (entity is null)
                return false;
            entity.BankAccountBalanc = model.BankAccountBalanc;
            _context.Banks.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
