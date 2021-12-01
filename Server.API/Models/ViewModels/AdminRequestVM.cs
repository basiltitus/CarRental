using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.API.Models.ViewModels
{
    public class AdminRequestVM
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int ChargePerDay { get; set; }
        public int CarModelId { get; set; }
        public int CarId { get; set; }
        public string RegNo { get; set; }
        public string CarName { get; set; }
        public string Colour { get; set; }
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string ImgUrl { get; set; }
        public string Status { get; set; }

    }
}
