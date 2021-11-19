using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalPortal.Models
{
    public class CarModel
    {
        public int CarModelId { get; set; }
        [Required(ErrorMessage = "Please enter Car Name")]
        [Display(Name = "Name of the car")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Car Name must be of maximum 30 charecters long")]
        public string CarName { get; set; }
        [Required(ErrorMessage = "Transmission type required")]
        [Display(Name ="Transmission Type")]
        public CarTransmission CarTransmission { get; set; }
        [Required(ErrorMessage = "Please Select Car Type")]
        [Display(Name = "Car Type")]
        public CarVarient CarType { get; set; }
        [Required(ErrorMessage = "Please enter No of seats")]
        [Range(1, 10, ErrorMessage = "Seat count must be between {1} and {2}.")]
        [Display(Name = "Seat Count")]
        public int SeatCount { get; set; }
        [Required(ErrorMessage = "Please enter Charge per day")]
        [Range(100, 10000,ErrorMessage = "Daily rental must be between {1} and {2}.")]
        [Display(Name = "Rent Per Day")]
        public int ChargePerDay { get; set; }
        [Display(Name = "User ID")]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User UserDetails { get; set; }
        public DateTime CreatedOn { get; set; }
        public CarModel()
        {
            UserDetails = new User();
        }

    }
    public enum CarVarient
    {
        MINI_HATCHBACK, SMALL_HATCHBACKS, Small_Sedan, Sedans, Executive_Luxury_Cars, MPVs, SUV, Crossovers
    }
    public enum CarTransmission
    {
        Manual, Automatic
    }
}
