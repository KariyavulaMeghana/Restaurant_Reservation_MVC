using Restaurant_Reservation_MVC.IServices;
using Restaurant_Reservation_MVC.Models;
using System.Text.Json;
using System.Text;

namespace Restaurant_Reservation_MVC.Services
{
    public class ReviewService : IReviewService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public ReviewService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<string> AddReview(Review reviewCreate)
        {
            var reviewJson = new StringContent(JsonSerializer.Serialize(reviewCreate), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_config["ApiSettings:AuthURLReviews"], reviewJson);
            response.EnsureSuccessStatusCode();
            return "Review added successfully";
        }

        public async Task<string> UpdateReview(Review updatereview)
        {
            var reviewJson = new StringContent(JsonSerializer.Serialize(updatereview), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_config["ApiSettings:AuthURLReviews"]}{updatereview.ReviewID}", reviewJson);
            response.EnsureSuccessStatusCode();
            return "Review updated successfully";
        }

        public async Task<string> DeleteReview(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_config["ApiSettings:AuthURLReviews"]}{id}");
            response.EnsureSuccessStatusCode();
            return "Review deleted successfully";
        }

        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            var response = await _httpClient.GetAsync(_config["ApiSettings:AuthURLReviews"]);
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<Review>>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<Review> GetReviewById(int id)
        {
            var response = await _httpClient.GetAsync($"{_config["ApiSettings:AuthURLReviews"]}{id}");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<Review>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        public async Task<IEnumerable<Review>> GetReviewsByRestaurantId(int restaurantId)
        {
            var response = await _httpClient.GetAsync($"{_config["ApiSettings:AuthURLReviews"]}restaurant/{restaurantId}");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<Review>>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserId(int userId)
        {
            var response = await _httpClient.GetAsync($"{_config["ApiSettings:AuthURLReviews"]}user/{userId}");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<Review>>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
