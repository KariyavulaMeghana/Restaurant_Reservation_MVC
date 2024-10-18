using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_MVC.Models
{
    public class Restaurant
    {
        [Key]
        public int RestaurantID { get; set; }
        [Required(ErrorMessage = "Enter your Name")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "name should contains 3 to 15 chars")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "only letters are allowed")]
        public string? RestaurantName { get; set; }
        [Required(ErrorMessage = "enter description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "enter your location")]
        public string? Location { get; set; }
        [Required(ErrorMessage = "enter your PhoneNumber")]
        [Phone(ErrorMessage = "Invaid phone number")]
        public string? ContactNumber { get; set; }
        public double Rating { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
