using ACHSimulartor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHSimulartor.Domain.Dtos
{
  public class UpdateTransferRequestStatusDto
    {
        public int Id { get; set; }

        public EnumStatus Status { get; set; }
        public string FromShebaNumber { get; set; }
        public string ToShebaNumber { get; set; }
        public decimal Price { get; set; }

    }
}
