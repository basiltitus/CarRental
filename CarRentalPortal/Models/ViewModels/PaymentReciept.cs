using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalPortal.Models
{
    public class PaymentReciept
    {
       public int OrderId { get; set; }
       public string Type { get; set; }
       public int Total { get; set; }
       public int ExtraDays { get; set; }
    }
}
