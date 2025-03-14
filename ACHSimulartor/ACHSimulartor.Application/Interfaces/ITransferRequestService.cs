using ACHSimulartor.Domain.Dtos;
using ACHSimulartor.Domain.Entites;
using ACHSimulartor.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHSimulartor.Application.Interfaces
{
    public interface ITransferRequestService
    {
        Task<Result<TransferRequestsDto>> CreateTransferRequestAsync(CreateTransferRequestDto model);
        Task<Result<List<TransferRequestsDto>>> GetAllTransferRequestsAsync();
        Task<Result<TransferRequestsDetailsDto>> GetTransferRequestAsync(int id);
        Task<Result> CanceledTransferRequestAsync(int id);
        Task<Result> ConfirmedTransferRequestAsync(int id);
    }
}