using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalPortal.Models
{
    public class SecurityTable
    {
        [Required(ErrorMessage = "Please enter your security code")]
        public string SecurityCode { get; set; }
    }
}
