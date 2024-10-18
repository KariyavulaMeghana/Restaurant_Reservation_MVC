using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Restaurant_Reservation_MVC.Models
{
    public class Reservation
    {
        [Key]
        [JsonPropertyName("reservationId")]
        public int ReservationId { get; set; }
        public virtual User? User { get; set; }
        [ForeignKey(nameof(User))]
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        public virtual Restaurant? Restaurant { get; set; }
        [ForeignKey(nameof(Restaurant))]
        [JsonPropertyName("restaurantId")]
        public int RestaurantId { get; set; }

        [JsonPropertyName("reservationDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Reservation Date")]
        public DateTime ReservationDate { get; set; }


        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }
}
