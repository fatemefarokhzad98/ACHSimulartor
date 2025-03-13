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
    public class TransferRequestRepository(ACHSimulartorDbContext _context) : ITransferRequestRepository
    {
        public Task<int> CreateTransferRequestAsync(TransferRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<List<TransferRequest>> GetAllTransferRequestsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TransferRequest> GetTransferRequestByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTransferRequestStatusAsync(TransferRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
