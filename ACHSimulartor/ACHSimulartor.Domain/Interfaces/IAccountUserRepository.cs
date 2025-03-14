using ACHSimulartor.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHSimulartor.Domain.Interfaces
{
    public interface IAccountUserRepository
    {
        Task<bool> HasSufficientBalanceAsync(string shebaNo,decimal price);
        Task<bool> UpdateAccountUserAsync(AccountUser model);
        Task<AccountUser?> GetAccountUserByShebaNumber(string shebaNumber);


    }
}
