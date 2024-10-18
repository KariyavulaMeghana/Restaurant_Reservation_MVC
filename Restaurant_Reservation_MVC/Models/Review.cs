using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_MVC.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }
        public virtual User? User { get; set; }
        [ForeignKey(nameof(User))]
        public int UserID { get; set; }
        public virtual Restaurant? Restaurant { get; set; }
        [ForeignKey(nameof(Restaurant))]
        public int RestaurantID { get; set; }
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
