using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Server.API.Models
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

        [Display(Name = "Car ID")]
        [ForeignKey("Car")]
        [Required(ErrorMessage = "Please select a Car Model")]
        public int CarModelId { get; set; }

        [Display(Name = "User ID")]
        [ForeignKey("User")]
        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
        public bool Active { get; set; }
    }
}
