using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalPortal.Models.ViewModels
{
    public class ConfirmPageVM
    {
        public CarListVM carListVM{ get; set; }
        public List<Coupon> Coupons { get; set; }
    }
}
