using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.API.Models.ViewModels
{
    public class OrderHistoryVM
    {
        public int OrderId { get; set; }
        public int CarId { get; set; }
        public int CarModelId { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }
        public string CarName { get; set; }
        public string ImgUrl { get; set; }
        public int Total { get; set; }
        public int CouponId { get; set; }
        public string CouponName { get; set; }
        public int Discount { get; set; }
    }
}
