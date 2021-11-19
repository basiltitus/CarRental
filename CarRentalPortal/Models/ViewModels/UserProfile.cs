using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalPortal.Models
{
    public class UserProfile
    {
        public string token { get; set; }
        public int userId { get; set; }
        public string role { get; set; }
        public string ImgUrl { get; set; }
        public string Name { get; set; }
    }
}
