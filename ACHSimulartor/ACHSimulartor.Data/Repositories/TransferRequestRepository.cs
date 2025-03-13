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
    public class TransferRequestRepository(ACHSimulartorDbContext _context) : ITransferRequestRepository
    {
        public async Task<int> CreateTransferRequestAsync(TransferRequest model)
        {
            await _context.TransferRequests.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<List<TransferRequest>?> GetAllTransferRequestsAsync()
        {
            return await _context.TransferRequests.AsNoTracking().ToListAsync();
        }

        public async Task<TransferRequest?> GetTransferRequestByIdAsync(int id)
        {
            return await _context.TransferRequests.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool?> UpdateTransferRequestStatusAsync(TransferRequest model)
        {
            TransferRequest? entity = await _context.TransferRequests.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
            if (entity is null)
                return false;
            entity.Staus = model.Staus;
            _context.TransferRequests.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
