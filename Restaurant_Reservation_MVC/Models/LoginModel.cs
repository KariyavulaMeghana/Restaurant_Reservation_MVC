using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_MVC.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter User Name")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password cannot exceed 100 characters.")]
        public string? Password { get; set; }
    }
}
