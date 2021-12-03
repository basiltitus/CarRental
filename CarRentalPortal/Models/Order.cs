using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalPortal.Models
{
    public class Order : IValidatableObject
    {
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }

        [Display(Name = "User ID")]
        public int UserId { get; set; }

        [Display(Name = "Car ID")]
        [ForeignKey("Car")]
        [Required(ErrorMessage = "Please select a car")]
        public int CarId { get; set; }

        public CarModel CarDetail { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "From Date is required")]
        public DateTime FromDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [Required(ErrorMessage = "From Date is required")]
        public DateTime ToDate { get; set; }

        [Display(Name = "Total Charge")]
        public int Total { get; set; }

        [Display(Name = "Extra Days")]
        public int ExtraDays { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name ="Fine Amount")]
        public int FineAmount { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public int CouponId { get; set; } = 0;
        public string CouponName { get; set; }
        public int Discount { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ToDate < FromDate)
            {
                yield return new ValidationResult(
                    errorMessage: "End Date must be greater than Start Date",
                    memberNames: new[] { "ToDate" }
               );
            }
        }
    }
}
