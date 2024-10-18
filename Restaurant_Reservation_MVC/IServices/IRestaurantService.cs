using Restaurant_Reservation_MVC.Models;

namespace Restaurant_Reservation_MVC.IServices
{
    public interface IRestaurantServices
    {
        Task<string> AddNewRestaurant(Restaurant restaurant);
        Task<string> UpdateRestaurant(Restaurant Updaterestaurant);
        Task<string> DeleteRestaurant(int restaurantId);
        Task<Restaurant> GetRestaurantById(int restaurantId);
        Task<List<Restaurant>> GetAllRestaurants();
        Task<List<Restaurant>> GetRestaurantByLocation(string location);
        Task<List<Restaurant>> SearchRestaurants(string name, string location);
    }
}
