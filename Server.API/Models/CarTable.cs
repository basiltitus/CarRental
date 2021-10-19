using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.API.Models
{
    public class CarTable
    {
        public int CarId { get; set; }
        [Required(ErrorMessage = "Please enter Car Name")]
        public string CarName { get; set; }
        [Required(ErrorMessage = "Please enter Car Registration Number")]
        public string CarRegNo { get; set; }
        [Required(ErrorMessage = "Please Select Car Type")]
        public CarVarient CarType { get; set; }
        [Required(ErrorMessage = "Please enter Charge per day")]
        public int ChargePerDay { get; set; }
    }
    public enum CarVarient
    {
        MINI_HATCHBACK, SMALL_HATCHBACKS, Small_Sedan, Sedans, Executive_Luxury_Cars, MPVs, SUV, Crossovers
    }
}
