using ACHSimulartor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHSimulartor.Domain.Entites
{
    public class Bank
    {
        public int BankCode { get; set; }
        public decimal BankAccountBalance { get; set; }
        public List<TransferRequest> TransferRequests { get; set; }

    }
}
