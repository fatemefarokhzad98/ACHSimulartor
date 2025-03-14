using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHSimulartor.Domain.Dtos
{
  public  class TransferRequestsDetailsDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string FromShebaNumber { get; set; }
        public string ToShebaNumber { get; set; }
    }
}
