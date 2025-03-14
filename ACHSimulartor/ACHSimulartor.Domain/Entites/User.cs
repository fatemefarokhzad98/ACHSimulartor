using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHSimulartor.Domain.Entites
{
    public class User
    {
        public string UserShebaNumber { get; set; }
        public decimal AccountBalance { get; set; }
        public decimal BankAccountBalance { get; set; }
        public List<Transaction> Transactions { get; set; } = [];
        public List<TransferRequest> TransferRequests { get; set; } = [];

    }
}
