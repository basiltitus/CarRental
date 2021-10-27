using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalPortal.Models
{
    public class CarTable
    {
        public int CarId { get; set; }
        [Required(ErrorMessage = "Please enter Car Name")]
        [Display(Name = "Name of the car")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Car Name must be of maximum 30 charecters long")]
        public string CarName { get; set; }
        [Required(ErrorMessage = "Please enter Car Registration Number")]
        [Display(Name = "Registration Number")]
        [RegularExpression("^[A-Z]{2}\\-[0-9]{2}\\-[A-Z]{1,2}\\-[0-9]{4}$", ErrorMessage = "Registration number must be properly formatted.")]
        public string CarRegNo { get; set; }
        [Required(ErrorMessage = "Please Select Car Type")]
        [Display(Name = "Car Type")]
        public CarVarient CarType { get; set; }
        [Required(ErrorMessage = "Please enter Charge per day")]
        [Range(0, 10000,ErrorMessage = "Daily rental must be between {1} and {2}.")]
        [Display(Name = "Rent Per Day")]
        public int ChargePerDay { get; set; }
    }
    public enum CarVarient
    {
        MINI_HATCHBACK, SMALL_HATCHBACKS, Small_Sedan, Sedans, Executive_Luxury_Cars, MPVs, SUV, Crossovers
    }
}
