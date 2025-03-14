using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHSimulartor.Domain.Shared
{
    public static class SuccessMessages
    {
        public const string SuccessfullyDone = "Request is saved successfully and is in pending status";
        public const string UpdateUserAccountSuccessfullyDone = "Update UserAccount SuccessfullyDone";
        public const string ValidationSuccessfullyDone = "Validation Create TransferRequest SuccessfullyDone";
        public const string ListRequestSuccessfullyDone = "Get All Request SuccessfullyDone";
        public const string UpdateRequestSuccessfullyDone = " Update Request Status SuccessfullyDone";

    }
    public static class ErrorMessages
    {
        public const string OperationFailedError = "Operation Failed";
        public const string NotFoundError = "NotFound";
        public const string ShebaError = "ShebaNumber Invalid";
        public const string InsufficientInventoryError = "InsufficientInventory";
        public const string DepreciationFailedError = "Depreciation Failed";
        public const string UnspecifiedTransactionError = "The Transaction is Unknown";
        public const string ShebaIncorrectedError = "The ShebaNmber Incorrected";
        public const string BankNotFoundError = "The Bank NotFound";
        public const string UserAccountNotFoundError = "UserAccount NotFound";
        public const string BadRequestError = "RequestNotFound";
        public const string RequestNotFoundError = "No Request Found";
        public const string UpdateRequestError = "Update TransferRequest Status Failed";
        public const string UpdateUserAccountError = "Update UserAccount  Failed";
        public const string UpdateBankAccountError = "Update BankAccount  Failed";
        public const string TransactionError = "Create Transaction  Failed";
        public const string ReservtionError = "BankResertion is   Failed";
    }
}
