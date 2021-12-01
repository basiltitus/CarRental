using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalPortal.Models
{
    public class Car
    {
        public int CarId { get; set; }

        [Required(ErrorMessage = "Please enter Registration Number")]
        [RegularExpression("[a-zA-Z]{2}[-]{1}[0-9]{2}[-]{1}[a-zA-Z]{1,2}[-]{1}[0-9]{4}",
        ErrorMessage = "Registration Number is required and must be properly formatted.")]
        public string RegNo { get; set; }
        [Required(ErrorMessage = "Please enter colour of the car")]
        public string Colour { get; set; }
        [DataType(DataType.Url)]
        [Required(ErrorMessage ="Car Image is required")]
        public string ImgUrl { get; set; }

        [Display(Name = "Car Model ID")]
        [ForeignKey("Car")]
        [Required(ErrorMessage = "Please select a Car Model")]
        public int CarModelId { get; set; }

        [Display(Name = "User ID")]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public DateTime CreatedOn { get; set; }
        public bool Active { get; set; }
    }
}
