using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.API.Models
{
    public class User
    { /*[Key]*/
        public int UserId { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter email address")]
        [EmailAddress]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Email must be of maximum 50 charecters long")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be 6-20 charecters long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Mobile number must be 10 digits long")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter your License Number")]
        public string LicenseNumber { get; set; }
        public string Role { get; set; }
        public string ImgUrl { get; set; } = "";
    }
}
