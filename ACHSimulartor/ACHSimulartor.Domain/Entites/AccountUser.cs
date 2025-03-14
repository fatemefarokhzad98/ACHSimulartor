using ACHSimulartor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHSimulartor.Domain.Entites
{
   public class AccountUser
    {
        public string ShebaNumber { get; set; }
        public decimal AccountBalance { get; set; }
        public EnumTransaction Transaction { get; set; }
        public List<TransferRequest> TransferRequests { get; set; }
    }
}
