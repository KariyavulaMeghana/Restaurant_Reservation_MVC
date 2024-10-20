using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_MVC.IServices;
using Restaurant_Reservation_MVC.Models;
using System.Text.Json;
using System.Text;
using Restaurant_Reservation_MVC.DTO;


namespace Restaurant_Reservation_MVC.Services
{
    public class ReservationService : IReservationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly string _apiUrl;

        public ReservationService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            _apiUrl = _config["ApiSettings:AuthURL"];
        }

        public async Task<string> BookReservationAsync(Reservation reservation)
        {
            var reservationJson = new StringContent(JsonSerializer.Serialize(reservation), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_apiUrl, reservationJson);
            response.EnsureSuccessStatusCode();
            return "Reservation booked successfully";
        }

        public async Task<string> UpdateReservationAsync(int id, Reservation updatedReservation)
        {
            var reservationJson = new StringContent(JsonSerializer.Serialize(updatedReservation), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_apiUrl}{id}/update", reservationJson);
            response.EnsureSuccessStatusCode();
            return "Reservation updated successfully";
        }

        public async Task<string> CancelReservationAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}{id}");
            response.EnsureSuccessStatusCode();
            return "Reservation canceled successfully";
        }

        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<Reservation>>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        //public async Task<List<Reservation>> GetReservationsAsync()
        //{
        //    var response = await _httpClient.GetAsync(_apiUrl);
        //    response.EnsureSuccessStatusCode();

        //    var jsonString = await response.Content.ReadAsStringAsync();
        //    return JsonSerializer.Deserialize<List<Reservation>>(jsonString);
        //}


        //public async Task<Reservation?> GetReservationByIdAsync(int id)
        //{
        //    var response = await _httpClient.GetAsync($"{_apiUrl}{id}");
        //    response.EnsureSuccessStatusCode();
        //    var responseStream = await response.Content.ReadAsStreamAsync();
        //    return await JsonSerializer.DeserializeAsync<Reservation>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //}
        public async Task<ReservationDTO?> GetReservationByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}{id}");

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error Response: {errorResponse}");
                return null;
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Raw JSON Response: {jsonString}"); // Log the raw response

            // Check if the response is empty or contains non-JSON content
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                Console.WriteLine("Empty or null JSON response.");
                return null;
            }

            // Basic check to ensure the response starts with '{' (object) or '[' (array)
            if (!jsonString.TrimStart().StartsWith("{") && !jsonString.TrimStart().StartsWith("["))
            {
                Console.WriteLine("Non-JSON response detected. Response content:");
                Console.WriteLine(jsonString);
                return null;
            }

            try
            {
                // Deserialize the JSON array and get the first element as a single ReservationDTO
                var reservationList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ReservationDTO>>(jsonString);
                return reservationList?.FirstOrDefault(); // Return the first reservation or null
            }

            catch (Exception ex)
            {
                // Log any other unexpected errors
                // Log any other unexpected errors
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                return null;
            }
        }


        public async Task<List<Reservation>> GetReservationsByUserIdAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}user/{userId}");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<Reservation>>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<Reservation>> GetReservationsByRestaurantIdAsync(int restaurantId)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}restaurant/{restaurantId}");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<Reservation>>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<Reservation>> GetReservationsByDateAsync(DateTime date)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}date/{date:yyyy-MM-dd}");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<Reservation>>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }


    }
}