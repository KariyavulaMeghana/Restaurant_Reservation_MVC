using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_MVC.DTO
{
        public class ReservationDTO
        {
            [Key]
            public int ReservationId { get; set; }
            [Required]
            public int UserId { get; set; }
            [Required]
            public int RestaurantId { get; set; }
            public DateTime ReservationDate { get; set; }
            public string? Status { get; set; }
        } 
}
