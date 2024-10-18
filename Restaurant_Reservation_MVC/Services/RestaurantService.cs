using Restaurant_Reservation_MVC.IServices;
using Restaurant_Reservation_MVC.Models;
using System.Text.Json;
using System.Text;

namespace Restaurant_Reservation_MVC.Services
{
    public class RestaurantServices : IRestaurantServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public RestaurantServices(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }


        public async Task<string> AddNewRestaurant(Restaurant restaurant)
        {
            var restaurantJson = new StringContent(JsonSerializer.Serialize(restaurant), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_config["ApiSettings:AuthURLRestaurant"], restaurantJson);
            response.EnsureSuccessStatusCode();
            return "Restaurant added successfully";

        }

        public async Task<string> DeleteRestaurant(int restaurantId)
        {
            var response = await _httpClient.DeleteAsync($"{_config["ApiSettings:AuthURLRestaurant"]}{restaurantId}");
            response.EnsureSuccessStatusCode();
            return "Restaurant deleted successfully";

        }

        public async Task<List<Restaurant>> GetAllRestaurants()
        {
            var response = await _httpClient.GetAsync(_config["ApiSettings:AuthURLRestaurant"]);
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<Restaurant>>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        }

        public async Task<Restaurant> GetRestaurantById(int restaurantId)
        {
            var response = await _httpClient.GetAsync($"{_config["ApiSettings:AuthURLRestaurant"]}{restaurantId}");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<Restaurant>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        }

        public async Task<List<Restaurant>> GetRestaurantByLocation(string location)
        {
            var response = await _httpClient.GetAsync($"{_config["ApiSettings:AuthURLRestaurant"]}location/{location}");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<Restaurant>>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }



        public async Task<List<Restaurant>> SearchRestaurants(string name, string location)
        {
            var response = await _httpClient.GetAsync($"{_config["ApiSettings:AuthURLRestaurant"]}search?name={name}&location={location}");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<Restaurant>>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        }

        public async Task<string> UpdateRestaurant(Restaurant Updaterestaurant)
        {
            var restaurantJson = new StringContent(JsonSerializer.Serialize(Updaterestaurant), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_config["ApiSettings:AuthURLRestaurant"]}{Updaterestaurant.RestaurantID}", restaurantJson);
            response.EnsureSuccessStatusCode();
            return "Restaurant updated successfully";

        }
    }
}
