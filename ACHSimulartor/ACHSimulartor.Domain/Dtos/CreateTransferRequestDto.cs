using ACHSimulartor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHSimulartor.Domain.Dtos
{
    public class CreateTransferRequestDto
    {
        [Required]
        public decimal Price { get; set; }
        [Required]
        [RegularExpression(@"IR[0-9]{24}")]
        public string FromShebaNumber { get; set; }
        [Required]
        [RegularExpression(@"IR[0-9]{24}")]
        public string ToShebaNumber { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Note { get; set; }
    }
}
