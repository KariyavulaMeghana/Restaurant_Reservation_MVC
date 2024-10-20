using Restaurant_Reservation_MVC.DTO;

namespace Restaurant_Reservation_MVC.Models
{
    public class AdminDashBoard
    {
        public List<UserDTO> UserDTOs { get; set; }
        public List<Restaurant> Restaurants { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<Review> Reviews { get; set; }
        public List<MenuItem> MenuItems { get; set; }
    }
}
