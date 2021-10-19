using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalPortal.Models
{
    public class PaymentTable
    {
        [Display(Name = "Payment ID")]
        public int PaymentId { get; set; }
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }
        [Display(Name = "User ID")]
        public int UserId { get; set; }
        [Display(Name = "Total Charge")]
        public int Total { get; set; }
    }
}
