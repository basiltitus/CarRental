using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.API.Models
{
    public class Coupon
    {
        [Key]
        public int CouponId { get; set; }
        public string CouponName { get; set; }
        public int MinOrderAmount { get; set; }
        public int PercentageDiscount { get; set; }
        public int MaxDiscount { get; set; }
        public bool Active { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
