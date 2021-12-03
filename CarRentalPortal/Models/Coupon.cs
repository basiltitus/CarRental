using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalPortal.Models
{
    public class Coupon
    {
        [Key]
        public int CouponId { get; set; }
        [Display(Name ="Coupon Code")]
        public string CouponName { get; set; }
        [Display(Name = "Minimum Order Amount")]
        public int MinOrderAmount { get; set; }
        [Display(Name = "Discount Percentage")]
        public int PercentageDiscount { get; set; }
        [Display(Name = "Maximum Discount")]
        public int MaxDiscount { get; set; }
        public bool Active { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
