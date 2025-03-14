using ACHSimulartor.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHSimulartor.Domain.Interfaces
{
   public interface IUserRepository
    {
        Task<bool> UpdateAccountUserAsync(User model);
        Task<bool> HasSufficientBalanceAsync(string shebaNo, decimal price);
        Task<User?> GetAccountUserAsync(string shebaNumber);
    }
}
