using ACHSimulartor.Domain.Dtos;
using ACHSimulartor.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHSimulartor.Domain.Interfaces
{
    public interface ITransferRequestRepository
    {

         Task<int> CreateTransferRequestAsync(TransferRequest model);
        Task<bool?> UpdateTransferRequestStatusAsync(TransferRequest model);
        Task<List<TransferRequest>?> GetAllTransferRequestsAsync();
        Task<TransferRequest?> GetTransferRequestByIdAsync(int id);

    }
}
