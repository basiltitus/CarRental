using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalPortal.Models.ViewModels
{
    public class ForgotPasswordVM
    {
        [Required(ErrorMessage ="Please enter your registered EmailId")]
        [EmailAddress]
        public string EmailId { get; set; }
    }
}
