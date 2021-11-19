using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Server.API.Models
{
    public class CarModel
    {
        public int CarModelId { get; set; }
        [Required(ErrorMessage = "Please enter Car Name")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Car Name must be of maximum 30 charecters long")]
        public string CarName { get; set; }
        [Required(ErrorMessage = "Transmission type required")]
        public CarTransmission CarTransmission { get; set; }

        [Required(ErrorMessage = "Please Select Car Type")]
        public CarVarient CarType { get; set; }
        [Required(ErrorMessage = "Please enter No of seats")]
        public int SeatCount { get; set; }
        [Required(ErrorMessage = "Please enter Charge per day")]
        public int ChargePerDay { get; set; }
        [ForeignKey("User")]
        [Required]
        public int UserId { get; set; }

        public User UserDetails { get; set; }
        [Required]
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
        Manual,Automatic
    }
}
