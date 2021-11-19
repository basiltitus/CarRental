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
        public string RegNo { get; set; }
        [Required(ErrorMessage = "Please enter colour of the car")]
        public string Colour { get; set; }
        [DataType(DataType.Url)]
        public string ImgUrl { get; set; }

        [Display(Name = "Car Model ID")]
        [ForeignKey("Car")]
        [Required(ErrorMessage = "Please select a Car Model")]
        public int CarModelId { get; set; }

        [Display(Name = "User ID")]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public DateTime CreatedOn { get; set; }
       
    }
}
