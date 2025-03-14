using ACHSimulartor.Data.Context;
using ACHSimulartor.Domain.Entites;
using ACHSimulartor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHSimulartor.Data.Repositories
{
    public class TransactionRepository(ACHSimulartorDbContext _context) : ITransactionRepository
    {
        public async Task<int> CreateTransactionAsync(Transaction model)
        {
           await _context.Transactions.AddAsync(model);
            if (model.Id == 0)
                return 0;
            await _context.SaveChangesAsync();
            return model.Id;
        }
    }
}
