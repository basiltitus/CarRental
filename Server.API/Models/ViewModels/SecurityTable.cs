using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.API.Models
{
    public class SecurityTable
    {
        [Required(ErrorMessage = "Please enter your security code")]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "Invalid security code")]
        public string SecurityCode { get; set; }
    }
}
