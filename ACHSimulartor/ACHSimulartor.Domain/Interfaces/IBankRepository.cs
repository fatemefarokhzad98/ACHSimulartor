using ACHSimulartor.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHSimulartor.Domain.Interfaces
{
   public interface IBankRepository
    {
        Task<bool> UpdateBankAccountBalanceAsync(Bank model);
        Task<Bank?> GetBankByBankCodeAsync(int bankCode);
    }
}
