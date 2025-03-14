using ACHSimulartor.Application.Interfaces;
using ACHSimulartor.Domain.Dtos;
using ACHSimulartor.Domain.Entites;
using ACHSimulartor.Domain.Enums;
using ACHSimulartor.Domain.Interfaces;
using ACHSimulartor.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ACHSimulartor.Application.Services
{
    public class TransferRequestService(ITransferRequestRepository _transferRequest, IAccountUserRepository _accountUser, IBankRepository _bank) : ITransferRequestService
    {
        public async Task<Result> CreateTransferRequestAsync(CreateTransferRequestDto model)
        {
            #region Validation
            var validation=await ValidationCreateTransferRequest(model);
            if (validation.IsFailure)
                return validation;
            #endregion

            #region UpdateUserAccount  
            var ResultUpdateUserAccount = await UpdateUserAccountAsync(model.FromShebaNumber, model.Price);
            if (ResultUpdateUserAccount.IsFailure)
                return ResultUpdateUserAccount;
            #endregion

            #region UpdateBank
            var ResultUpdateBank = await UpdateBankAsync(model.ToShebaNumber, model.Price);
            if (ResultUpdateBank.IsFailure)
                return ResultUpdateBank;

            #endregion

            #region CreateTransferRequest
            TransferRequest entity = new()
            {
                FromShebaNumber = model.FromShebaNumber,
                Id = 0,
                Note = model.Note,
                Price = model.Price,
                ToShebaNumber = model.ToShebaNumber,
                Staus = EnumStaus.pending,
                BankCode = ResultUpdateBank.Value
            };
            var resultCreateTransferRequest = await _transferRequest.CreateTransferRequestAsync(entity);
            if (resultCreateTransferRequest < 1)
                return Result.Failure(ErrorMessages.OperationFailedError);
            return Result.Success(SuccessMessages.SuccessfullyDone);
            #endregion

        }

        public async Task<Result<List<TransferRequestsDto>>> GetAllTransferRequestsAsync()
        {
            var entities = await _transferRequest.GetAllTransferRequestsAsync();
            #region Validation
            if (entities is null || entities.Count == 0)
                return Result.Failure<List<TransferRequestsDto>>(ErrorMessages.RequestNotFound);
            #endregion
            var sortedAndConditionedEntities = entities.Where(x=>x.Staus==EnumStaus.pending).OrderBy(e => e.CreatedAt);
            var dtoList = sortedAndConditionedEntities.Select(entity => new TransferRequestsDto()
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
                FromShebaNumber = entity.FromShebaNumber,
                Note = entity.Note,
                Price = entity.Price,
                Staus = entity.Staus,
                ToShebaNumber = entity.ToShebaNumber
            }).ToList();
            return Result.Success<List<TransferRequestsDto>>(dtoList,SuccessMessages.ListRequestSuccessfullyDone);
        }

        public async Task<Result<TransferRequestsDetailsDto>> GetTransferRequestAsync(int id)
        {
            var entity = await _transferRequest.GetTransferRequestByIdAsync(id);
            #region Validation
            if (entity is null )
                return Result.Failure<TransferRequestsDetailsDto>(ErrorMessages.RequestNotFound);
            #endregion
            TransferRequestsDetailsDto dtoModel = new()
            {
                Id = entity.Id,
                FromShebaNumber = entity.FromShebaNumber,
                Price = entity.Price,
                ToShebaNumber = entity.ToShebaNumber
            };
            return Result.Success<TransferRequestsDetailsDto>(dtoModel, SuccessMessages.ListRequestSuccessfullyDone);
        }
           
        public Task<Result> UpdateTransferRequestAsync(UpdateTransferRequestStatusDto model)
        {
            throw new NotImplementedException();
        }
        public Task<Result<TransferRequestsDto>> CanceledTransferRequestAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<TransferRequestsDto>> ConfirmedTransferRequestAsync(int id)
        {
            throw new NotImplementedException();
        }


        #region Private Method
        private bool IsValidShebaNumber(string shebaNumber)
        {
            if (shebaNumber.StartsWith("IR") && shebaNumber.Length == 26)
            {
                var numericPart = shebaNumber.Substring(2);
                if (numericPart.All(char.IsDigit))
                {
                    return true;
                }
            }
            return false;
        }
        private decimal SubFromAccountBalance(decimal price, decimal accountBalance)
        {
            return price - accountBalance;

        }
        private decimal AddToAccountBalance(decimal price, decimal accountBalance)
        {
            return price + accountBalance;

        }
        private int GetBankCodeByShebaNubmer(string shebaNumber)
        {
            if (shebaNumber.Length == 26)
            {
                var bankCode = shebaNumber.Substring(4, 3);
                return Convert.ToInt32(bankCode);
            }

            return 0;
        }
        private decimal ReservationToBankAccount(decimal price, decimal accountBalance)
        {
            return price + accountBalance;

        }
        private async Task<Result> ValidationCreateTransferRequest(CreateTransferRequestDto model)
        {
            if (model is null)
                return Result.Failure(ErrorMessages.NotFoundError);
            if (IsValidShebaNumber(model.FromShebaNumber) == false || IsValidShebaNumber(model.ToShebaNumber) == false)
                return Result.Failure(ErrorMessages.ShebaIncorrected);
            if (await _accountUser.HasSufficientBalanceAsync(model.FromShebaNumber, model.Price) == false)
                return Result.Failure(ErrorMessages.InsufficientInventoryError);
            return Result.Success(SuccessMessages.ValidationSuccessfullyDone);
        }
        private async Task<Result> UpdateUserAccountAsync(string shebaNumber, decimal price)
        {
            var accountUser = await _accountUser.GetAccountUserByShebaNumber(shebaNumber);
            if (accountUser is null)
                return Result.Failure(ErrorMessages.UserAccountNotFound);
            accountUser.AccountBalance = SubFromAccountBalance(price, accountUser.AccountBalance);
            accountUser.Transaction = EnumTransaction.Depreciation;
            var accountUserResult = await _accountUser.UpdateAccountUserAsync(accountUser);
            if (accountUserResult is false)
                return Result.Failure(ErrorMessages.DepreciationFailedError);
            return Result.Success(SuccessMessages.UpdateUserAccountSuccessfullyDone);
        }
        private async Task<Result<int>> UpdateBankAsync(string shebaNumber, decimal price)
        {
            var bankCode = GetBankCodeByShebaNubmer(shebaNumber);
            if (bankCode == 0)
                return Result.Failure<int>(ErrorMessages.BankNotFound);
            var bankEntity = await _bank.GetBankByBankCodeAsync(bankCode);
            if (bankEntity is null)
                return Result.Failure<int>(ErrorMessages.BankNotFound);
            bankEntity.BankAccountBalance = ReservationToBankAccount(price, bankEntity.BankAccountBalance);
            var resultReservationBank = await _bank.UpdateBankAccountBalanceAsync(bankEntity);
            if (resultReservationBank == false)
                return Result.Failure<int>(ErrorMessages.UnspecifiedTransaction);
            return bankCode;
        }
        #endregion



    } 
}
