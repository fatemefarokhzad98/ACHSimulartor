using ACHSimulartor.Domain.Entites;
using ACHSimulartor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHSimulartor.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<int> CreateTransactionAsync(Transaction model);

      
    }
}
