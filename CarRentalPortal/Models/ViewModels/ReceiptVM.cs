using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalPortal.Models.ViewModels
{
    public class ReceiptVM
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime DOB{ get; set; }
        public string PhoneNumber { get; set; }
        public string LicenseNumber { get; set; }
        public int CarId { get; set; }
        public string RegNo { get; set; }
        public string Colour { get; set; }
        public string ImgUrl { get; set; }
        public int CarModelId { get; set; }
        public string CarName { get; set; }
        public CarTransmission CarTransmission{ get; set; }
        public CarVarient CarType { get; set; }
        public int SeatCount { get; set; }
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }
        public int Total { get; set; }
        public string Status { get; set; }
        public int PaymentId { get; set; }
        public int FineAmount { get; set; }
        public int ExtraDays { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public int ChargePerDay { get; set; }

    }


}
