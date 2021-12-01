using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Server.API.Models.ViewModels
{
    public class CarListVM
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

        public CarModel CarModelDetails { get; set; }
        [Display(Name = "User ID")]
        [ForeignKey("User")]
        [Required]
        public int UserId { get; set; }

        public User UserDetails { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        public bool Active { get; set; }
        public bool Available { get; set; }
        public string NextAvailable { get; set; }
        public CarListVM()
        {
            CarModelDetails = new CarModel();
            UserDetails = new User();
        }
    }
}
