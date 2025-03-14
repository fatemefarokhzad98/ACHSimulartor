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

    }
    public static class ErrorMessages
    {
        public const string OperationFailedError = "Operation Failed";
        public const string NotFoundError = "NotFound";
        public const string ShebaError = "ShebaNumber Invalid";
        public const string InsufficientInventoryError = "InsufficientInventory";
        public const string DepreciationFailedError = "Depreciation Failed";
        public const string UnspecifiedTransaction = "The Transaction is Unknown";
        public const string ShebaIncorrected = "The ShebaNmber Incorrected";
        public const string BankNotFound = "The Bank NotFound";
        public const string UserAccountNotFound = "UserAccount NotFound";
        public const string BadRequest = "RequestNotFound";
        public const string RequestNotFound = "No Request Found";
    }
}
