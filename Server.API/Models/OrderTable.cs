using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Server.API.Models
{
    public class OrderTable
    {
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("CarTable")]
        [Required]
        public int CarId { get; set; }
        public CarTable Cardetail { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }
        [Required]
        public int Total { get; set; }
        public int ExtraDays { get; set; }
        [Required]
        public bool Completed { get; set; }
        public OrderTable()
        {
            Cardetail = new CarTable();
        }
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
