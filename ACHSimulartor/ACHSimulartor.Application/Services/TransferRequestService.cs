using ACHSimulartor.Application.Interfaces;
using ACHSimulartor.Domain.Dtos;
using ACHSimulartor.Domain.Enums;
using ACHSimulartor.Domain.Interfaces;
using ACHSimulartor.Domain.Shared;
using EntityUsing = ACHSimulartor.Domain.Entites;
namespace ACHSimulartor.Application.Services
{
    public class TransferRequestService(ITransferRequestRepository _transferRequest, IUserRepository _AccountUser, ITransactionRepository _transaction) : ITransferRequestService
    {
        public async Task<Result<TransferRequestsDto>> CreateTransferRequestAsync(CreateTransferRequestDto model)
        {
            #region Validation
            var validation = await ValidationCreateTransferRequest(model);
            if (validation.IsFailure)
            return Result.Failure<TransferRequestsDto>(ErrorMessages.BadRequestError);
            #endregion
            #region CreateTransferRequest
            EntityUsing.TransferRequest entity = new()
            {
                FromShebaNumber = model.FromShebaNumber,
                Id = 0,
                Note = model.Note,
                Price = model.Price,
                ToShebaNumber = model.ToShebaNumber,
                Status = EnumStatus.pending,
                UserShebaNumber = model.FromShebaNumber
            };
            var resultCreateTransferRequest = await _transferRequest.CreateTransferRequestAsync(entity);
            if (resultCreateTransferRequest < 1)
                return Result.Failure<TransferRequestsDto>(ErrorMessages.OperationFailedError);
            #endregion
            #region UpdateAccountUser  
            var ResultUpdateAccountUser = await DepreciationAccountUserAsync(model.FromShebaNumber, model.Price);
            if (ResultUpdateAccountUser.IsFailure)
                return Result.Failure<TransferRequestsDto>(ErrorMessages.UpdateUserAccountError);
            #endregion

            #region Bank Reservtion 
            var resultReservtionBank = await ReservationToBankAccount(model.ToShebaNumber, model.Price);
            if (resultReservtionBank is false)
                return Result.Failure<TransferRequestsDto>(ErrorMessages.ReservtionError);
            #endregion



            #region AddTransaction
            var TransactionId = await CreateDepreciationTransactionAsync(resultCreateTransferRequest, model.FromShebaNumber, model.Price);
            if (TransactionId == 0)
                return Result.Failure<TransferRequestsDto>(ErrorMessages.TransactionError);
            #endregion
            TransferRequestsDto dto = new()
            {
                CreatedAt = entity.CreatedAt,
                Id = resultCreateTransferRequest,
                FromShebaNumber = entity.FromShebaNumber,
                Note = entity.Note,
                Price = entity.Price,
                Status = entity.Status,
                ToShebaNumber = entity.ToShebaNumber


            };
            return Result.Success<TransferRequestsDto>(dto, SuccessMessages.SuccessfullyDone);

        }

        public async Task<Result<List<TransferRequestsDto>>> GetAllTransferRequestsAsync()
        {
            var entities = await _transferRequest.GetAllTransferRequestsAsync();
            #region Validation
            if (entities is null || entities.Count == 0)
                return Result.Failure<List<TransferRequestsDto>>(ErrorMessages.RequestNotFoundError);
            #endregion
            var sortedAndConditionedEntities = entities.Where(x => x.Status == EnumStatus.pending).OrderBy(e => e.CreatedAt);
            var dtoList = sortedAndConditionedEntities.Select(entity => new TransferRequestsDto()
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
                FromShebaNumber = entity.FromShebaNumber,
                Note = entity.Note,
                Price = entity.Price,
                Status = entity.Status,
                ToShebaNumber = entity.ToShebaNumber
            }).ToList();
            return Result.Success<List<TransferRequestsDto>>(dtoList, SuccessMessages.ListRequestSuccessfullyDone);
        }

        public async Task<Result<TransferRequestsDetailsDto>> GetTransferRequestAsync(int id)
        {
            var entity = await _transferRequest.GetTransferRequestByIdAsync(id);
            #region Validation
            if (entity is null)
                return Result.Failure<TransferRequestsDetailsDto>(ErrorMessages.RequestNotFoundError);
            #endregion
            TransferRequestsDetailsDto dtoModel = new()
            {
                Id = entity.Id,
                FromShebaNumber = entity.FromShebaNumber,
                Price = entity.Price,
                ToShebaNumber = entity.ToShebaNumber
            };
            return Result.Success<TransferRequestsDetailsDto>(dtoModel, SuccessMessages.GetRequestSuccessfullyDone);
        }

        public async Task<Result> CanceledTransferRequestAsync(int id)
        {
            if (id == 0)
                return Result.Failure(ErrorMessages.RequestNotFoundError);
            var entity = await _transferRequest.GetTransferRequestByIdAsync(id);
            if (entity is null)
                return Result.Failure(ErrorMessages.RequestNotFoundError);
            entity.Status = EnumStatus.canceled;
            var resultUpdate = await _transferRequest.UpdateTransferRequestStatusAsync(entity);
            if (resultUpdate is false)
                return Result.Failure(ErrorMessages.UpdateRequestError);

            var resultUpdateBank = await UpdateSubFromBankAsync(entity.ToShebaNumber, entity.Price);
            if (resultUpdateBank is false)
                return Result.Failure(ErrorMessages.UpdateBankAccountError);

            var resultUpdateUserAccount = await RefundUserAccountAsync(entity.FromShebaNumber, entity.Price);
            var TransactionId = await CreateRefundTransactionAsync(id, entity.FromShebaNumber, entity.Price);
            if (TransactionId == 0)
                return Result.Failure(ErrorMessages.TransactionError);

            return Result.Success(SuccessMessages.UpdateRequestSuccessfullyDone);

        }

        public async Task<Result> ConfirmedTransferRequestAsync(int id)
        {
            if (id == 0)
                return Result.Failure(ErrorMessages.RequestNotFoundError);
            var entity = await _transferRequest.GetTransferRequestByIdAsync(id);
            if (entity is null)
                return Result.Failure(ErrorMessages.RequestNotFoundError);
            entity.Status = EnumStatus.confirmed;
            var resultUpdate = await _transferRequest.UpdateTransferRequestStatusAsync(entity);
            if (resultUpdate is false)
                return Result.Failure(ErrorMessages.UpdateRequestError);

            var accountUser = await _AccountUser.GetAccountUserAsync(entity.ToShebaNumber);
            if (accountUser is null)
                return Result.Failure(ErrorMessages.UserAccountNotFoundError);
            accountUser.AccountBalance = AddToAccountBalance(entity.Price, accountUser.AccountBalance);
            accountUser.BankAccountBalance = SubFromBankAccount(entity.Price, accountUser.BankAccountBalance);
            var resultUpdateUserAccount = await _AccountUser.UpdateAccountUserAsync(accountUser);
            if (resultUpdateUserAccount is false)
                return Result.Failure(ErrorMessages.UpdateUserAccountError);
            return Result.Success(SuccessMessages.UpdateRequestSuccessfullyDone);
        }


        #region Private Method
        private bool IsValidShebaNumber(string shebaNumber)
        {
            var start = shebaNumber.Substring(0, 2);
            if (start == "IR" && shebaNumber.Length == 26)
            {
                var numericPart = shebaNumber.Substring(2);
                if (numericPart.All(char.IsDigit))
                {
                    return true;
                }
            }
            return false;
        }
        private async Task<int> CreateDepreciationTransactionAsync(int requestId, string shebaNumber, decimal price)
        {
            Domain.Entites.Transaction transaction = new()
            {
                Price = price,
                TransactionType = EnumTransaction.Depreciation,
                TransferRequestId = requestId,
                UserShebaNumber = shebaNumber

            };
            var resultCreatetransaction = await _transaction.CreateTransactionAsync(transaction);
            return resultCreatetransaction;
        }
        private async Task<int> CreateRefundTransactionAsync(int requestId, string shebaNumber, decimal price)
        {
            EntityUsing.Transaction transaction = new()
            {
                Price = price,
                TransactionType = EnumTransaction.Refund,
                TransferRequestId = requestId,
                UserShebaNumber = shebaNumber
            };
            var resultCreatetransaction = await _transaction.CreateTransactionAsync(transaction);
            return resultCreatetransaction;
        }
        private decimal SubFromAccountBalance(decimal price, decimal accountBalance)
        {
            return accountBalance - price;

        }
        private decimal AddToAccountBalance(decimal price, decimal accountBalance)
        {
            return price + accountBalance;

        }

        private async Task<bool> ReservationToBankAccount(string shebaNumber, decimal price)
        {
            var user = await _AccountUser.GetAccountUserAsync(shebaNumber);
            if (user is null)
                return false;
            user.BankAccountBalance = user.BankAccountBalance + price;
            return await _AccountUser.UpdateAccountUserAsync(user);
        }
        private decimal SubFromBankAccount(decimal price, decimal accountBalance)
        {
            return accountBalance - price;

        }
        private async Task<Result> ValidationCreateTransferRequest(CreateTransferRequestDto model)
        {
            if (model is null)
                return Result.Failure(ErrorMessages.BadRequestError);
            if (model.Price == 0)
                return Result.Failure(ErrorMessages.BadRequestError);
            if (IsValidShebaNumber(model.FromShebaNumber) == false || IsValidShebaNumber(model.ToShebaNumber) == false)
                return Result.Failure(ErrorMessages.ShebaIncorrectedError);
            if (await _AccountUser.HasSufficientBalanceAsync(model.FromShebaNumber, model.Price) == false)
                return Result.Failure(ErrorMessages.InsufficientInventoryError);
            return Result.Success(SuccessMessages.ValidationSuccessfullyDone);
        }
        private async Task<Result> DepreciationAccountUserAsync(string shebaNumber, decimal price)
        {
            var accountUser = await _AccountUser.GetAccountUserAsync(shebaNumber);
            if (accountUser is null)
                return Result.Failure(ErrorMessages.UserAccountNotFoundError);
            accountUser.AccountBalance = SubFromAccountBalance(price, accountUser.AccountBalance);
            var ResultaccountUser = await _AccountUser.UpdateAccountUserAsync(accountUser);
            if (ResultaccountUser is false)
                return Result.Failure(ErrorMessages.DepreciationFailedError);
            return Result.Success(SuccessMessages.UpdateUserAccountSuccessfullyDone);
        }
        private async Task<bool> RefundUserAccountAsync(string shebaNumber, decimal price)
        {
            var accountUser = await _AccountUser.GetAccountUserAsync(shebaNumber);
            if (accountUser is null)
                return false;
            accountUser.AccountBalance = AddToAccountBalance(price, accountUser.AccountBalance);

            return await _AccountUser.UpdateAccountUserAsync(accountUser);
        }
        private async Task<bool> UpdateSubFromBankAsync(string shebaNumber, decimal price)
        {
            var accountUser = await _AccountUser.GetAccountUserAsync(shebaNumber);
            if (accountUser is null)
                return false;
            accountUser.BankAccountBalance = SubFromBankAccount(price, accountUser.BankAccountBalance);
            var resultUpdateUser = await _AccountUser.UpdateAccountUserAsync(accountUser);
            return resultUpdateUser;
        }

        #endregion



    }
}
