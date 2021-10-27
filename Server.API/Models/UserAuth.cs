using System.ComponentModel.DataAnnotations;

namespace Server.API.Models
{
    public class UserAuth
    {
        [Required(ErrorMessage = "Please enter email address")]
        [EmailAddress]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Email must be of maximum 50 charecters long")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be 6-20 charecters long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
