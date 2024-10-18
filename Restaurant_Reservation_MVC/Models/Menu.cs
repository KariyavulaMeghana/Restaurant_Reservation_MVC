using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_MVC.Models
{
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuId { get; set; }
        public virtual Restaurant? Restaurant { get; set; }

        [Required(ErrorMessage = "Restaurant ID is required.")]
        [ForeignKey(nameof(Restaurant))]
        public int RestaurantId { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}
