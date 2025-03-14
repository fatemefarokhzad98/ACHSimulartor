using ACHSimulartor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHSimulartor.Domain.Entites
{
    public class TransferRequest
    {
        
        public int Id { get; set; }
        public string UserShebaNumber { get; set; }
        public decimal Price { get; set; }
        public string FromShebaNumber { get; set; }
        public string ToShebaNumber { get; set; } 
        public string? Note { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public EnumStatus Status { get; set; }
        public User User { get; set; }
        public List<Transaction> Transactions { get; set; } = [];


    }
}
