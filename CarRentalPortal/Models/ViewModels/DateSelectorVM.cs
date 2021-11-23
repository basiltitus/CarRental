using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalPortal.Models.ViewModels
{
    public class DateSelectorVM
    {
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "From Date is required")]
        public DateTime FromDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [Required(ErrorMessage = "From Date is required")]
        public DateTime ToDate { get; set; }
    }
}
