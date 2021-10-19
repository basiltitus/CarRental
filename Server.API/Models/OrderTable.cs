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
        
        public int UserId { get; set; }
        [ForeignKey("CarTable")]
        public int CarId { get; set; }
        public CarTable Cardetail { get; set; }
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }
        public int Total { get; set; }
        public int ExtraDays { get; set; }
        public bool Completed { get; set; }
        public OrderTable()
        {
            Cardetail = new CarTable();
        }
    }
}
