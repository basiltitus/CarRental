using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalPortal.Models.ViewModels
{
    public class PasswordChangeVM
    {
        [Required(ErrorMessage ="OTP is required")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "OTP will be of 6 digits!")]
        public string OTP{ get; set; }
        [Required(ErrorMessage = "Please enter password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be 6-20 charecters long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be 6-20 charecters long")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
