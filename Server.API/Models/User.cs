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
        [Required(ErrorMessage = "Please enter email address")]
        [EmailAddress]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be 6-20 charecters long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter your Aadhar Number")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "ID Number will be of 12 charecters length")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "ID card number holds only numbers")]
        public string AadharNumber { get; set; }
    }
}
