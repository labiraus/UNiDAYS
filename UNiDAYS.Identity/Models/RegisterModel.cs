using System.ComponentModel.DataAnnotations;

namespace UNiDAYS.Identity.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please enter Username.")]
        public string UserName { get; set; }
        
        [DataType(DataType.Password)]
        [MinLength(6), Required(ErrorMessage = "Please enter Password.")]

        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
