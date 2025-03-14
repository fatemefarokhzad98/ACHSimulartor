using ACHSimulartor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHSimulartor.Domain.Entites
{
  public class Transaction
    {
        public int Id { get; set; }
        public string UserShebaNumber { get; set; }
        public decimal Price { get; set; }
        public EnumTransaction TransactionType { get; set; }
        public int TransferRequestId { get; set; }
        public TransferRequest TransferRequest { get; set; }
        public User User { get; set; }
    }
}
